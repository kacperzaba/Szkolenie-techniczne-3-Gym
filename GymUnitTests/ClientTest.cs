using Gym.Client.CrossCutting.Dtos;
using Gym.Client.Resolvers;
using Gym.Client.Services;
using Gym.Client.Storage;
using Gym.Client.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GymUnitTests
{
    public class CustomerTest
    {
        private CustomerService GetServiceWithInMemoryDb(string dbName, out ClientDbContext context)
        {
            var options = new DbContextOptionsBuilder<ClientDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            context = new ClientDbContext(null!);
            context.Database.EnsureCreated();
            context.Customers.RemoveRange(context.Customers);
            context.SaveChanges();

            var dummyResolver = (HttpSubscriptionResolver)null!;
            return new CustomerService(context, dummyResolver);
        }

        [Fact]
        public async Task Add_ShouldAddCustomer()
        {
            var service = GetServiceWithInMemoryDb("AddCustomerDb", out var context);

            var dto = new CreateCustomerDto
            {
                FirstName = "Anna",
                LastName = "Nowak",
                Email = "anna@example.com",
                PhoneNumber = "123456789",
                Address = "Main Street 1"
            };

            var result = await service.Add(dto);

            Assert.NotNull(result);
            Assert.Single(context.Customers);
            Assert.Equal("Anna", result.FirstName);
        }

        [Fact]
        public async Task GetById_ShouldReturnCustomerWithSubscriptions()
        {
            var service = GetServiceWithInMemoryDb("GetByIdCustomerDb", out var context);

            var customer = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                PhoneNumber = "999888777",
                Address = "Anywhere 12"
            };

            customer.CustomerSubscriptions.Add(new CustomerSubscription
            {
                ExternalId = 5,
                SubscriptionType = "Monthly",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1)
            });

            context.Customers.Add(customer);
            context.SaveChanges();

            var result = await service.GetById(customer.Id);

            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
            Assert.Single(result.Subscriptions);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllCustomers()
        {
            var service = GetServiceWithInMemoryDb("GetAllCustomersDb", out var context);

            context.Customers.AddRange(
                new Customer { FirstName = "A", LastName = "B", Email = "a@a.com", PhoneNumber = "123", Address = "Addr" },
                new Customer { FirstName = "C", LastName = "D", Email = "c@c.com", PhoneNumber = "456", Address = "Addr" }
            );
            context.SaveChanges();

            var result = await service.GetAll();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Update_ShouldChangeCustomer()
        {
            var service = GetServiceWithInMemoryDb("UpdateCustomerDb", out var context);

            var customer = new Customer
            {
                FirstName = "Old",
                LastName = "Name",
                Email = "old@email.com",
                PhoneNumber = "000000",
                Address = "OldStreet"
            };
            context.Customers.Add(customer);
            context.SaveChanges();

            var dto = new UpdateCustomerDto
            {
                FirstName = "New",
                LastName = "Name",
                Email = "new@email.com",
                PhoneNumber = "111111",
                Address = "NewStreet"
            };

            var result = await service.Update(customer.Id, dto);

            Assert.NotNull(result);
            Assert.Equal("New", result.FirstName);
            Assert.Equal("111111", result.PhoneNumber);
        }

        [Fact]
        public async Task Delete_ShouldRemoveCustomer()
        {
            var service = GetServiceWithInMemoryDb("DeleteCustomerDb", out var context);

            var customer = new Customer
            {
                FirstName = "ToDelete",
                LastName = "Me",
                Email = "delete@me.com",
                PhoneNumber = "000",
                Address = "X"
            };
            context.Customers.Add(customer);
            context.SaveChanges();

            var result = await service.Delete(customer.Id);

            Assert.True(result);
            Assert.Null(await context.Customers.FindAsync(customer.Id));
        }
    }
}
