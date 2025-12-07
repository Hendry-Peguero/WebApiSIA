using Microsoft.AspNetCore.Mvc;
using WebApiSIA.Core.Application.Dtos.ItemGruop;
using WebApiSIA.Core.Application.Interfaces.Services;
using WebApiSIA.Core.Domain.Entities;

namespace WebApiSIA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemGruopController : ControllerBase
    {
        private readonly IGenericService<
            SaveItemGruopDto,
            ItemGruopDto,
            ItemGroupEntity> _service;

        public ItemGruopController(
            IGenericService<SaveItemGruopDto, ItemGruopDto, ItemGroupEntity> service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ItemGruopDto>>> Get()
        {
            try
            {
                var data = await _service.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error al obtener los grupos de ítems", error = ex.Message });
            }
        }
    }
}
