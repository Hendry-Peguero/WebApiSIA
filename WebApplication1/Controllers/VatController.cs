using Microsoft.AspNetCore.Mvc;
using WebApiSIA.Core.Application.Dtos.Vat;
using WebApiSIA.Core.Application.Interfaces.Services;
using WebApiSIA.Core.Domain.Entities;

namespace WebApiSIA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VatController : ControllerBase
    {
        private readonly IGenericService<
            SaveVatDto,
            VatDto,
            VatEntity> _service;

        public VatController(
            IGenericService<SaveVatDto, VatDto, VatEntity> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<VatDto>>> Get()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }
    }
}