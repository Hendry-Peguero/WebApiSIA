using Microsoft.AspNetCore.Mvc;
using WebApiSIA.Core.Application.Dtos.WareHouse;
using WebApiSIA.Core.Application.Interfaces.Services;
using WebApiSIA.Core.Domain.Entities;

namespace WebApiSIA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WareHouseController : ControllerBase
    {
        private readonly IGenericService<
            SaveWareHouseDto,
            WareHouseDto,
            WareHouseEntity> _service;

        public WareHouseController(IGenericService<SaveWareHouseDto, WareHouseDto, WareHouseEntity> service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<WareHouseDto>>> Get()
        {
            try
            {
                var data = await _service.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error al obtener los valores de WareHouse", error = ex.Message });
            }
        }
    }
}
