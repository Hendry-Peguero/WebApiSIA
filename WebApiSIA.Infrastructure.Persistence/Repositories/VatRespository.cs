using WebApiSIA.Core.Application.Interfaces.Repositories;
using WebApiSIA.Core.Domain.Entities;
using WebApiSIA.Infrastructure.Persistence.Contexts;

namespace WebApiSIA.Infrastructure.Persistence.Repositories
{
    public class VatRespository : GenericRepository<VatEntity>, IVatRespository
    {
        private readonly ApplicationContext _context;

        public VatRespository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
