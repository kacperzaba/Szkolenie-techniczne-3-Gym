using Gym.FitnessClass.CrossCutting.Dtos;
using Gym.FitnessClass.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gym.FitnessClass.Controllers
{
    [ApiController]
    [Route("api/fitnessclass")]
    public class FitnessClassController : ControllerBase
    {
        private readonly IFitnessClassService _service;

        public FitnessClassController(IFitnessClassService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var fc = await _service.GetById(id);
            if (fc == null) return NotFound();
            return Ok(fc);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFitnessClassDto dto)
        {
            var created = await _service.Add(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFitnessClassDto dto)
        {
            var updated = await _service.Update(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpPost("{id}/customer/{custId}")]
        public async Task<IActionResult> AssignCustomer(int id, int custId)
        {
            var result = await _service.AssignCustomer(id, custId);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            return result ? NoContent() : NotFound();
        }

    }
}
