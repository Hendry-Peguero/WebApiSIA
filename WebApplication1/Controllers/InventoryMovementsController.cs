using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using WebApiSIA.Core.Application.Dtos;
using WebApiSIA.Core.Application.Dtos.InventoryMovement;
using WebApiSIA.Core.Application.Interfaces.Helpers;
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

        private readonly ISqlHelper _sqlHelper;

        public InventoryMovementsController(
            IGenericService<SaveInventoryMovementDto, InventoryMovementDto, InventoryMovementEntity> service,
            ISqlHelper sqlHelper)
        {
            _service = service;
            _sqlHelper = sqlHelper;
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

        // CREATE / AJUSTE DE INVENTARIO
        [HttpPost("adjust-inventory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AdjustInventory([FromBody] AdjustInventoryRequestDto request)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@ITEM_ID",       request.ITEM_ID },
                    { "@Movement_Type", request.Movement_Type },
                    { "@Quantity",      request.Quantity },
                    { "@WarehouseID",   request.WarehouseID },
                    { "@SHELF_ID",      request.SHELF_ID },
                    { "@CreatedBy",     request.CreatedBy },
                    { "@Reason",        request.Reason }
                };

                _sqlHelper.ExecuteSQLStoredProcedure("sp_AdjustInventory", parameters);

                return Ok(new
                {
                    message = "Inventario ajustado correctamente."
                });
            }
            catch (SqlException ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = ex.Message
                });
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
