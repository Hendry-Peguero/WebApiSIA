using WebApiSIA.Core.Application.Interfaces.Repositories;
using WebApiSIA.Core.Domain.Entities;
using WebApiSIA.Infrastructure.Persistence.Contexts;

namespace WebApiSIA.Infrastructure.Persistence.Repositories
{
    public class ItemInformationRepository : GenericRepository<ItemInformationEntity>, IItemInformationRepository
    {
        private readonly ApplicationContext context;

        public ItemInformationRepository(ApplicationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
