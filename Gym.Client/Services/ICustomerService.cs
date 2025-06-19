using Gym.Client.CrossCutting.Dtos;

namespace Gym.Client.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto> Add(CreateCustomerDto dto);
        Task<IEnumerable<CustomerDto>> GetAll();
        Task<CustomerWithSubscriptionsDto> GetById(int id);
        Task<CustomerDto> Update(int id, UpdateCustomerDto dto);
        Task<bool> AssignSubscription(int customerId, int subscriptionId);
        Task<bool> Delete(int id);
    }
}
