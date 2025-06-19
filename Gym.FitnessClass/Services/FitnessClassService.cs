using Gym.FitnessClass.CrossCutting.Dtos;
using Gym.FitnessClass.Resolvers;
using Gym.FitnessClass.Storage.Entities;
using Gym.FitnessClass.Storage;
using Microsoft.EntityFrameworkCore;

namespace Gym.FitnessClass.Services
{
    public class FitnessClassService : IFitnessClassService
    {
        private readonly FitnessClassDbContext _context;
        private readonly HttpCustomerResolver _resolver;

        public FitnessClassService(FitnessClassDbContext context, HttpCustomerResolver resolver)
        {
            _context = context;
            _resolver = resolver;
        }

        public async Task<FitnessClassDto> Add(CreateFitnessClassDto dto)
        {
            var fc = new Storage.Entities.FitnessClass
            {
                Name = dto.Name,
                Description = dto.Description,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };
            _context.FitnessClasses.Add(fc);
            await _context.SaveChangesAsync();

            return new FitnessClassDto
            {
                Id = fc.Id,
                Name = fc.Name,
                Description = fc.Description,
                StartTime = fc.StartTime,
                EndTime = fc.EndTime
            };
        }

        public async Task<IEnumerable<FitnessClassDto>> GetAll()
        {
            return await _context.FitnessClasses
                .AsNoTracking()
                .Select(fc => new FitnessClassDto
                {
                    Id = fc.Id,
                    Name = fc.Name,
                    Description = fc.Description,
                    StartTime = fc.StartTime,
                    EndTime = fc.EndTime
                })
                .ToListAsync();
        }

        public async Task<FitnessClassWithCustomersDto?> GetById(int id)
        {
            var fc = await _context.FitnessClasses
                .Include(c => c.Participants)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (fc == null) return null;

            return new FitnessClassWithCustomersDto
            {
                Id = fc.Id,
                Name = fc.Name,
                Description = fc.Description,
                StartTime = fc.StartTime,
                EndTime = fc.EndTime,
                Participants = fc.Participants.Select(p => new CustomerDto
                {
                    Id = p.ExternalId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    PhoneNumber = p.PhoneNumber
                }).ToList()
            };
        }

        public async Task<FitnessClassDto?> Update(int id, UpdateFitnessClassDto dto)
        {
            var fc = await _context.FitnessClasses.FindAsync(id);
            if (fc == null) return null;

            fc.Name = dto.Name;
            fc.Description = dto.Description;
            fc.StartTime = dto.StartTime;
            fc.EndTime = dto.EndTime;
            await _context.SaveChangesAsync();

            return new FitnessClassDto
            {
                Id = fc.Id,
                Name = fc.Name,
                Description = fc.Description,
                StartTime = fc.StartTime,
                EndTime = fc.EndTime
            };
        }

        public async Task<bool> AssignCustomer(int fitnessClassId, int customerId)
        {
            var fc = await _context.FitnessClasses
                .Include(c => c.Participants)
                .FirstOrDefaultAsync(x => x.Id == fitnessClassId);
            if (fc == null) return false;

            var cust = await _resolver.GetCustomerByIdAsync(customerId);
            if (cust == null) return false;

            fc.Participants.Add(new FitnessClassCustomer
            {
                ExternalId = cust.Id,
                FitnessClassId = fc.Id,
                FirstName = cust.FirstName,
                LastName = cust.LastName,
                PhoneNumber = cust.PhoneNumber
            });
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
