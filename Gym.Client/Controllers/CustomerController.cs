using Gym.Client.CrossCutting.Dtos;
using Gym.Client.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gym.Client.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _service.GetAll();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _service.GetById(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
        {
            var created = await _service.Add(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerDto dto)
        {
            var updated = await _service.Update(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpPost("{id}/subscription/{subId}")]
        public async Task<IActionResult> AssignSubscription(int id, int subId)
        {
            var result = await _service.AssignSubscription(id, subId);
            return result ? NoContent() : NotFound();
        }
    }
}
