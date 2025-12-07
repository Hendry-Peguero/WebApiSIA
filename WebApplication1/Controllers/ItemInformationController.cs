using Microsoft.AspNetCore.Mvc;
using WebApiSIA.Core.Application.Dtos.ItemInformation;
using WebApiSIA.Core.Application.Interfaces.Services;
using WebApiSIA.Core.Domain.Entities;

namespace WebApiSIA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemInformationController : ControllerBase
    {
        private readonly IGenericService<
            SaveItemInformationDto,
            ItemInformationDto,
            ItemInformationEntity> _service;
        private readonly IItemInformationService _itemInformationService;

        public ItemInformationController(IGenericService<
            SaveItemInformationDto, 
            ItemInformationDto, 
            ItemInformationEntity> service,
            IItemInformationService itemInformationService)
        {
            _service = service;
            _itemInformationService = itemInformationService;
        }

        [HttpGet("barcode/{barcode}")]
        public async Task<ActionResult<ItemInformationDto>> GetByBarcode(string barcode)
        {
            var result = await _itemInformationService.GetByBarcodeAsync(barcode);

            if (result == null)
                return NotFound(new { message = $"No existe artículo con Barcode '{barcode}'." });

            return Ok(result);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ItemInformationDto>>> GetAll()
        {
            try
            {
                var items = await _service.GetAllAsync();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error al obtener los artículos", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ItemInformationDto>> GetById(int id)
        {
            try
            {
                var item = await _service.GetByIdAsync(id);

                if (item == null)
                    return NotFound(new { message = $"No se encontró el artículo con ID {id}" });

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error al obtener el artículo", error = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ItemInformationDto>> Create([FromBody] SaveItemInformationDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdItem = await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdItem.ITEM_ID },
                    createdItem);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error al crear el artículo", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ItemInformationDto>> Update(int id, [FromBody] SaveItemInformationDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updatedItem = await _service.UpdateAsync(id, dto);
                return Ok(updatedItem);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error al actualizar el artículo", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var item = await _service.GetByIdAsync(id);

                if (item == null)
                    return NotFound(new { message = $"No se encontró el artículo con ID {id}" });

                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error al eliminar el artículo", error = ex.Message });
            }
        }
    }
}