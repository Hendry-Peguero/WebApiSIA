using WebApiSIA.Core.Application.Interfaces.Repositories;
using WebApiSIA.Core.Domain.Entities;
using WebApiSIA.Infrastructure.Persistence.Contexts;

namespace WebApiSIA.Infrastructure.Persistence.Repositories
{
    public class ItemGroupRepository : GenericRepository<ItemGroupEntity>, IItemGroupRepository
    {
        private readonly ApplicationContext _context;

        public ItemGroupRepository (ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
