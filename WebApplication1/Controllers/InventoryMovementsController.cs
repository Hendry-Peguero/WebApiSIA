using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiSIA.Core.Application.Dtos.InventoryMovement;
using WebApiSIA.Core.Application.Interfaces.Services;
using WebApiSIA.Core.Domain.Entities;

namespace WebApiSIA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryMovementsController : ControllerBase
    {
        private readonly IGenericService<
            InventoryMovementSaveDto, 
            InventoryMovementDto, 
            InventoryMovementEntity> _service;

        public InventoryMovementsController(
            IGenericService<InventoryMovementSaveDto, InventoryMovementDto, InventoryMovementEntity> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<InventoryMovementDto>>> Get()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<InventoryMovementDto>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryMovementDto>> Post([FromBody] InventoryMovementSaveDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.MovementId }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<InventoryMovementDto>> Put(int id, [FromBody] InventoryMovementSaveDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}