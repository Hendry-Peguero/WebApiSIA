using WebApiSIA.Core.Application.Dtos.ItemInformation;
using WebApiSIA.Core.Domain.Entities;

namespace WebApiSIA.Core.Application.Interfaces.Repositories
{
    public interface IItemInformationRepository : IGenericRepository<ItemInformationEntity>
    {
        Task<ItemInformationEntity?> GetByBarcodeAsync(string barcode);
    }
}
