using Microsoft.AspNetCore.Mvc;
using WebApiSIA.Infrastructure.Persistence.Repositories;

namespace WebApiSIA.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class InventoryMovementsController : ControllerBase
    {
        private readonly InventoryMovementRepository _repo;

        public InventoryMovementsController(InventoryMovementRepository repo)
        {
            _repo = repo;
        }

        // GET: /api/InventoryMovements
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _repo.GetAllAsync();
            return Ok(data);
        }
    }
}
