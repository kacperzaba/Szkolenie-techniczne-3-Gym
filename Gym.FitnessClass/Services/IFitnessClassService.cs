using Gym.FitnessClass.CrossCutting.Dtos;

namespace Gym.FitnessClass.Services
{
    public interface IFitnessClassService
    {
        Task<FitnessClassDto> Add(CreateFitnessClassDto dto);
        Task<IEnumerable<FitnessClassDto>> GetAll();
        Task<FitnessClassWithCustomersDto?> GetById(int id);
        Task<FitnessClassDto?> Update(int id, UpdateFitnessClassDto dto);
        Task<bool> AssignCustomer(int fitnessClassId, int customerId);
        Task<bool> Delete(int id);
    }
}
