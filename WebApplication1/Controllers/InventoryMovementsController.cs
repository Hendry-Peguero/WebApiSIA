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
            SaveInventoryMovementDto,
            InventoryMovementDto,
            InventoryMovementEntity> _service;

        public InventoryMovementsController(IGenericService<
            SaveInventoryMovementDto,
            InventoryMovementDto,
            InventoryMovementEntity> service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<InventoryMovementDto>>> GetAll()
        {
            try
            {
                var movements = await _service.GetAllAsync();
                return Ok(movements);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error al obtener los movimientos de inventario", error = ex.Message });
            }
        }

        // GET BY ID
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<InventoryMovementDto>> GetById(int id)
        {
            try
            {
                var item = await _service.GetByIdAsync(id);

                if (item == null)
                    return NotFound(new { message = $"No se encontró el movimiento con ID {id}" });

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error al obtener el movimiento", error = ex.Message });
            }
        }

        // CREATE
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<InventoryMovementDto>> Create([FromBody] SaveInventoryMovementDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var created = await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = created.Movement_ID },
                    created
                );
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error al crear el movimiento", error = ex.Message });
            }
        }

        // UPDATE
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<InventoryMovementDto>> Update(int id, [FromBody] SaveInventoryMovementDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updated = await _service.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error al actualizar el movimiento", error = ex.Message });
            }
        }

        // DELETE
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existing = await _service.GetByIdAsync(id);

                if (existing == null)
                    return NotFound(new { message = $"No se encontró el movimiento con ID {id}" });

                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error al eliminar el movimiento", error = ex.Message });
            }
        }
    }
}
