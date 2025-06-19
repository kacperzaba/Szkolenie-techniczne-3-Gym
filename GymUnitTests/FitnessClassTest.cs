using Gym.FitnessClass.CrossCutting.Dtos;
using Gym.FitnessClass.Services;
using Gym.FitnessClass.Storage;
using Gym.FitnessClass.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Linq;
using System.Threading.Tasks;
using Gym.FitnessClass.Resolvers;

namespace GymUnitTests
{
    public class FitnessClassServiceTests
    {
        private FitnessClassService GetServiceWithInMemoryDb(string dbName, out FitnessClassDbContext context)
        {
            var options = new DbContextOptionsBuilder<FitnessClassDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            context = new FitnessClassDbContext(null!);
            context.Database.EnsureCreated();
            context.FitnessClasses.RemoveRange(context.FitnessClasses);
            context.SaveChanges();

            var dummyResolver = (HttpCustomerResolver)null!;
            return new FitnessClassService(context, dummyResolver);
        }

        [Fact]
        public async Task Add_ShouldAddFitnessClass()
        {
            var service = GetServiceWithInMemoryDb("AddDb", out var context);

            var dto = new CreateFitnessClassDto
            {
                Name = "Yoga",
                Description = "Stretching",
                StartTime = DateTime.Today,
                EndTime = DateTime.Today.AddHours(1)
            };

            var result = await service.Add(dto);

            Assert.NotNull(result);
            Assert.Single(context.FitnessClasses);
            Assert.Equal("Yoga", result.Name);
        }

        [Fact]
        public async Task GetById_ShouldReturnFitnessClassWithParticipants()
        {
            var service = GetServiceWithInMemoryDb("GetByIdDb", out var context);

            var fc = new FitnessClass
            {
                Name = "Zumba",
                Description = "Dance session",
                StartTime = DateTime.Today,
                EndTime = DateTime.Today.AddHours(1)
            };
            fc.Participants.Add(new FitnessClassCustomer
            {
                ExternalId = 1,
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "123456789"
            });

            context.FitnessClasses.Add(fc);
            context.SaveChanges();

            var result = await service.GetById(fc.Id);

            Assert.NotNull(result);
            Assert.Equal("Zumba", result.Name);
            Assert.Single(result.Participants);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllClasses()
        {
            var service = GetServiceWithInMemoryDb("GetAllDb", out var context);

            context.FitnessClasses.AddRange(
                new FitnessClass { Name = "A", Description = "DescA", StartTime = DateTime.Today, EndTime = DateTime.Today.AddHours(1) },
                new FitnessClass { Name = "B", Description = "DescB", StartTime = DateTime.Today, EndTime = DateTime.Today.AddHours(2) }
            );
            context.SaveChanges();

            var result = await service.GetAll();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Update_ShouldUpdateExistingClass()
        {
            var service = GetServiceWithInMemoryDb("UpdateDb", out var context);

            var fc = new FitnessClass
            {
                Name = "OldName",
                Description = "OldDesc",
                StartTime = DateTime.Today,
                EndTime = DateTime.Today.AddHours(1)
            };
            context.FitnessClasses.Add(fc);
            context.SaveChanges();

            var dto = new UpdateFitnessClassDto
            {
                Name = "NewName",
                Description = "NewDesc",
                StartTime = fc.StartTime,
                EndTime = fc.EndTime.AddHours(1)
            };

            var result = await service.Update(fc.Id, dto);

            Assert.NotNull(result);
            Assert.Equal("NewName", result.Name);
            Assert.Equal("NewDesc", result.Description);
        }

        [Fact]
        public async Task Delete_ShouldRemoveClass()
        {
            var service = GetServiceWithInMemoryDb("DeleteDb", out var context);

            var fc = new FitnessClass
            {
                Name = "DeleteMe",
                Description = "Temp",
                StartTime = DateTime.Today,
                EndTime = DateTime.Today.AddHours(1)
            };
            context.FitnessClasses.Add(fc);
            context.SaveChanges();

            var result = await service.Delete(fc.Id);

            Assert.True(result);
            Assert.Null(await context.FitnessClasses.FindAsync(fc.Id));
        }
    }
}
