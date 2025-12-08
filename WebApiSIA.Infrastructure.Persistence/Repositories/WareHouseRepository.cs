using WebApiSIA.Core.Application.Interfaces.Repositories;
using WebApiSIA.Core.Domain.Entities;
using WebApiSIA.Infrastructure.Persistence.Contexts;

namespace WebApiSIA.Infrastructure.Persistence.Repositories
{
    public class WareHouseRepository : GenericRepository<WareHouseEntity>, IWareHouseRepository
    {
        private readonly ApplicationContext _context;

        public WareHouseRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
