using WebApiSIA.Core.Application.Interfaces.Repositories;
using WebApiSIA.Core.Domain.Entities;
using WebApiSIA.Infrastructure.Persistence.Contexts;

namespace WebApiSIA.Infrastructure.Persistence.Repositories
{
    public class InventoryMovementRepository : GenericRepository<InventoryMovementEntity>, IInventoryMovementRepository
    {
        private readonly ApplicationContext _context;

        public InventoryMovementRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
