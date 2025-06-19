using Gym.Subscription.CrossCutting.Dtos;
using Gym.Subscription.Services;
using Gym.Subscription.Storage;
using Gym.Subscription.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymUnitTests
{
    public class SubscriptionTest
    {
        private SubscriptionService GetServiceWithInMemoryDb(string dbName, out SubscriptionDbContext context)
        {
            var options = new DbContextOptionsBuilder<SubscriptionDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            context = new SubscriptionDbContext(null!);
            context.Database.EnsureCreated();
            context.Subscriptions.RemoveRange(context.Subscriptions);
            context.SaveChanges();

            return new SubscriptionService(context);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddSubscription()
        {
            var service = GetServiceWithInMemoryDb("CreateDb", out var context);

            var dto = new CreateSubscriptionDto
            {
                SubscriptionType = "Monthly",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1),
                Amount = 100,
                IsPaid = true
            };

            var result = await service.CreateAsync(dto);

            Assert.NotNull(result);
            Assert.Single(context.Subscriptions);
            Assert.Equal("Monthly", result.SubscriptionType);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnSubscription()
        {
            var service = GetServiceWithInMemoryDb("GetByIdDb", out var context);

            var sub = new Subscription
            { 
                SubscriptionType = "Weekly",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(7),
                Amount = 50,
                IsPaid = false
            };
            context.Subscriptions.Add(sub);
            context.SaveChanges();

            var result = await service.GetByIdAsync(sub.Id);

            Assert.NotNull(result);
            Assert.Equal("Weekly", result.SubscriptionType);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllSubscriptions()
        {
            var service = GetServiceWithInMemoryDb("GetAllDb", out var context);

            context.Subscriptions.AddRange(new[]
            {
                new Subscription
                {
                    SubscriptionType = "Daily",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(1),
                    Amount = 10,
                    IsPaid = true
                },
                new Subscription
                {
                    SubscriptionType = "Yearly",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddYears(1),
                    Amount = 500,
                    IsPaid = false
                }
            });
            context.SaveChanges();

            var result = await service.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateSubscription()
        {
            var service = GetServiceWithInMemoryDb("UpdateDb", out var context);

            var sub = new Subscription
            {
                SubscriptionType = "Basic",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(30),
                Amount = 99,
                IsPaid = false
            };
            context.Subscriptions.Add(sub);
            context.SaveChanges();

            var dto = new UpdateSubscriptionDto
            {
                EndDate = sub.EndDate.AddDays(10),
                Amount = 149,
                IsPaid = true
            };

            var result = await service.UpdateAsync(sub.Id, dto);

            Assert.NotNull(result);
            Assert.Equal(149, result.Amount);
            Assert.True(result.IsPaid);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveSubscription()
        {
            var service = GetServiceWithInMemoryDb("DeleteDb", out var context);

            var sub = new Subscription
            {
                SubscriptionType = "Test",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(5),
                Amount = 30,
                IsPaid = false
            };
            context.Subscriptions.Add(sub);
            context.SaveChanges();

            var result = await service.DeleteAsync(sub.Id);

            Assert.True(result);
            Assert.Null(await context.Subscriptions.FindAsync(sub.Id));
        }
    }
}
