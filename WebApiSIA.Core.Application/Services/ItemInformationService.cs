using AutoMapper;
using WebApiSIA.Core.Application.Dtos.ItemInformation;
using WebApiSIA.Core.Application.Interfaces.Repositories;
using WebApiSIA.Core.Application.Interfaces.Services;
using WebApiSIA.Core.Domain.Entities;

namespace WebApiSIA.Core.Application.Services
{
    public class ItemInformationService : GenericService<SaveItemInformationDto, ItemInformationDto, ItemInformationEntity>, IItemInformationService
    {
        private readonly IItemInformationRepository _itemInformationRepository;
        private readonly IMapper _mapper;

        public ItemInformationService(
            IItemInformationRepository itemInformationRepository,
            IMapper mapper
        ) : base(itemInformationRepository, mapper)
        {
            _itemInformationRepository = itemInformationRepository;
            _mapper = mapper;
        }

        public async Task<ItemInformationDto?> GetByBarcodeAsync(string barcode)
        {
            var entity = await _itemInformationRepository.GetByBarcodeAsync(barcode);

            if (entity == null)
                return null;

            return _mapper.Map<ItemInformationDto>(entity);
        }
    }
}
