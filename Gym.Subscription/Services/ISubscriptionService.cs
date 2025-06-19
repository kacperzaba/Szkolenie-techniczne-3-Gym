using Gym.Subscription.CrossCutting.Dtos;

namespace Gym.Subscription.Services
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionDto>> GetAllAsync();
        Task<SubscriptionDto> GetByIdAsync(int id);
        Task<SubscriptionDto> CreateAsync(CreateSubscriptionDto dto);
        Task<SubscriptionDto> UpdateAsync(int id, UpdateSubscriptionDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
