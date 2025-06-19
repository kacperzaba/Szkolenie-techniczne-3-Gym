using Gym.Subscription.CrossCutting.Dtos;
using Gym.Subscription.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gym.Subscription.Controllers
{
    [ApiController]
    [Route("api/subscription")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _service;

        public SubscriptionController(ISubscriptionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subscriptions = await _service.GetAllAsync();
            return Ok(subscriptions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var subscription = await _service.GetByIdAsync(id);
            if (subscription == null) return NotFound();
            return Ok(subscription);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromBody] UpdateSubscriptionDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
