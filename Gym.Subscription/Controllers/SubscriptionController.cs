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

        /// <summary>
        /// Pobiera wszystkie subskrypcje.
        /// </summary>
        /// <returns>Lista wszystkich subskrypcji.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subscriptions = await _service.GetAllAsync();
            return Ok(subscriptions);
        }

        /// <summary>
        /// Pobiera subskrypcję na podstawie identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator subskrypcji.</param>
        /// <returns>Subskrypcja o podanym identyfikatorze lub 404 jeśli nie istnieje.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var subscription = await _service.GetByIdAsync(id);
            if (subscription == null) return NotFound();
            return Ok(subscription);
        }

        /// <summary>
        /// Tworzy nową subskrypcję.
        /// </summary>
        /// <param name="dto">Dane nowej subskrypcji.</param>
        /// <returns>Utworzona subskrypcja wraz z adresem zasobu.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        /// <summary>
        /// Aktualizuje istniejącą subskrypcję.
        /// </summary>
        /// <param name="id">Identyfikator subskrypcji do aktualizacji.</param>
        /// <param name="dto">Zaktualizowane dane subskrypcji.</param>
        /// <returns>Zaktualizowana subskrypcja lub 404 jeśli nie istnieje.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSubscriptionDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        /// <summary>
        /// Usuwa subskrypcję o podanym identyfikatorze.
        /// </summary>
        /// <param name="id">Identyfikator subskrypcji do usunięcia.</param>
        /// <returns>Kod 204 jeśli usunięto lub 404 jeśli subskrypcja nie istnieje.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
