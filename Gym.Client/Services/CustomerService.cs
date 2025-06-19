using Gym.Client.CrossCutting.Dtos;
using Gym.Client.Resolvers;
using Gym.Client.Storage;
using Gym.Client.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gym.Client.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ClientDbContext _context;
        private readonly HttpSubscriptionResolver _resolver;

        public CustomerService(ClientDbContext context, HttpSubscriptionResolver resolver)
        {
            _context = context;
            _resolver = resolver;
        }

        public async Task<CustomerDto> Add(CreateCustomerDto dto)
        {
            var customer = new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
        }

        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            return await _context.Customers
                .AsNoTracking()
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber
                })
                .ToListAsync();
        }

        public async Task<CustomerWithSubscriptionsDto> GetById(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.CustomerSubscriptions)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return null;

            return new CustomerWithSubscriptionsDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Subscriptions = customer.CustomerSubscriptions
                    .Select(s => new SubscriptionDto
                    {
                        Id = s.ExternalId,
                        SubscriptionType = s.SubscriptionType,
                        StartDate = s.StartDate,
                        EndDate = s.EndDate
                    })
                    .ToList()
            };
        }

        public async Task<CustomerDto> Update(int id, UpdateCustomerDto dto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return null;

            customer.FirstName = dto.FirstName;
            customer.LastName = dto.LastName;
            customer.Email = dto.Email;
            customer.PhoneNumber = dto.PhoneNumber;
            customer.Address = dto.Address;

            await _context.SaveChangesAsync();

            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
        }


        public async Task<bool> AssignSubscription(int customerId, int subscriptionId)
        {
            var customer = await _context.Customers
                .Include(c => c.CustomerSubscriptions)
                .FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer == null)
                return false;

            var subscription = await _resolver.GetSubscriptionByIdAsync(subscriptionId);
            if (subscription == null)
                return false;

            customer.CustomerSubscriptions.Add(new CustomerSubscription
            {
                ExternalId = subscription.Id,
                SubscriptionType = subscription.SubscriptionType,
                StartDate = subscription.StartDate,
                EndDate = subscription.EndDate
            });

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.CustomerSubscriptions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
