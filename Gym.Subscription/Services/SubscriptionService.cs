using Gym.Subscription.CrossCutting.Dtos;
using Gym.Subscription.Storage;
using Microsoft.EntityFrameworkCore;

namespace Gym.Subscription.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly SubscriptionDbContext _context;

        public SubscriptionService(SubscriptionDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAllAsync()
        {
            return await _context.Subscriptions
                .Select(s => new SubscriptionDto
                {
                    Id = s.Id,
                    SubscriptionType = s.SubscriptionType,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Amount = s.Amount,
                    IsPaid = s.IsPaid
                })
                .ToListAsync();
        }

        public async Task<SubscriptionDto> GetByIdAsync(int id)
        {
            return await _context.Subscriptions
                .Where(s => s.Id == id)
                .Select(s => new SubscriptionDto
                {
                    Id = s.Id,
                    SubscriptionType = s.SubscriptionType,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Amount = s.Amount,
                    IsPaid = s.IsPaid
                })
                .FirstOrDefaultAsync();
        }

        public async Task<SubscriptionDto> CreateAsync(CreateSubscriptionDto dto)
        {
            var subscription = new Storage.Entities.Subscription
            {
                SubscriptionType = dto.SubscriptionType,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Amount = dto.Amount,
                IsPaid = dto.IsPaid
            };

            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            return new SubscriptionDto
            {
                Id = subscription.Id,
                SubscriptionType = subscription.SubscriptionType,
                StartDate = subscription.StartDate,
                EndDate = subscription.EndDate,
                Amount = subscription.Amount,
                IsPaid = subscription.IsPaid
            };
        }

        public async Task<SubscriptionDto> UpdateAsync(int id, UpdateSubscriptionDto dto)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription == null)
                return null;

            subscription.EndDate = dto.EndDate;
            subscription.Amount = dto.Amount;
            subscription.IsPaid = dto.IsPaid;

            await _context.SaveChangesAsync();

            return new SubscriptionDto
            {
                Id = subscription.Id,
                SubscriptionType = subscription.SubscriptionType,
                StartDate = subscription.StartDate,
                EndDate = subscription.EndDate,
                Amount = subscription.Amount,
                IsPaid = subscription.IsPaid
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription == null)
                return false;

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
