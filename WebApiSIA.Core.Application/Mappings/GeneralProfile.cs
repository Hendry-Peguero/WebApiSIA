using AutoMapper;
using WebApiSIA.Core.Application.Dtos.InventoryMovement;
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
        }
    }
}