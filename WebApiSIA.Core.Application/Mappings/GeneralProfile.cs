using AutoMapper;
using WebApiSIA.Core.Application.Dtos.InventoryMovement;
using WebApiSIA.Core.Application.Dtos.ItemInformation;
using WebApiSIA.Core.Application.Dtos.User;
using WebApiSIA.Core.Domain.Entities;

namespace WebApiSIA.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            // Entity -> DTO
            CreateMap<InventoryMovementEntity, InventoryMovementDto>();

            // SaveDto -> Entity
            CreateMap<InventoryMovementSaveDto, InventoryMovementEntity>();

            // Opcional: para actualizar, ignorar MovementId
            CreateMap<InventoryMovementDto, InventoryMovementEntity>()
                .ForMember(dest => dest.MovementId, opt => opt.Ignore());

            // ========== ITEM INFORMATION ==========
            // Entity -> DTO (lectura)
            CreateMap<ItemInformationEntity, ItemInformationDto>();

            // DTO -> Entity (no debería usarse normalmente)
            CreateMap<ItemInformationDto, ItemInformationEntity>()
                .ForMember(dest => dest.ItemId, opt => opt.Ignore());

            // SaveDto -> Entity (para crear y actualizar)
            CreateMap<SaveItemInformationDto, ItemInformationEntity>()
                .ForMember(dest => dest.ItemId, opt => opt.Ignore());

            // UpdateDto -> Entity (si lo usas)
            CreateMap<UpdateItemInformationDto, ItemInformationEntity>()
                .ForMember(dest => dest.ItemId, opt => opt.Ignore());

            // Entity -> Entity (para copiar propiedades al actualizar)
            CreateMap<ItemInformationEntity, ItemInformationEntity>();

            CreateMap<UserEntity, UserDto>();
        }
    }
}