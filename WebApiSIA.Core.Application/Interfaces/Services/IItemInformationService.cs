using WebApiSIA.Core.Application.Dtos.ItemInformation;
using WebApiSIA.Core.Domain.Entities;

namespace WebApiSIA.Core.Application.Interfaces.Services
{
    public interface IItemInformationService : IGenericService<SaveItemInformationDto, ItemInformationDto, ItemInformationEntity>
    {
        Task<ItemInformationDto?> GetByBarcodeAsync(string barcode);
    }
}
