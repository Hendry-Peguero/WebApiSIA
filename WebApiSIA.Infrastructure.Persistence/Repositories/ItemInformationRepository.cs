using Microsoft.EntityFrameworkCore;
using WebApiSIA.Core.Application.Interfaces.Repositories;
using WebApiSIA.Core.Domain.Entities;
using WebApiSIA.Infrastructure.Persistence.Contexts;

namespace WebApiSIA.Infrastructure.Persistence.Repositories
{
    public class ItemInformationRepository : GenericRepository<ItemInformationEntity>, IItemInformationRepository
    {
        private readonly ApplicationContext _context;

        public ItemInformationRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ItemInformationEntity?> GetByBarcodeAsync(string barcode)
        {
            return await _context.ItemInformation
                  .FirstOrDefaultAsync(x => x.Barcode == barcode);
        }
    }
}
