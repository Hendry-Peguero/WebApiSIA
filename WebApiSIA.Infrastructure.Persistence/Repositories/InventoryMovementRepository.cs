using Microsoft.EntityFrameworkCore;
using WebApiSIA.Core.Domain.Entities;
using WebApiSIA.Infrastructure.Persistence.Contexts;

namespace WebApiSIA.Infrastructure.Persistence.Repositories
{
    public class InventoryMovementRepository
    {
        private readonly ApplicationContext _context;

        public InventoryMovementRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<InventoryMovement>> GetAllAsync()
        {
            return await _context.InventoryMovements
                .OrderByDescending(m => m.MovementDate)
                .ToListAsync();
        }
    }
}
