using AutoMapper;
using WebApiSIA.Core.Application.Dtos.InventoryMovement;
using WebApiSIA.Core.Application.Dtos.ItemGruop;
using WebApiSIA.Core.Application.Dtos.ItemInformation;
using WebApiSIA.Core.Application.Dtos.User;
using WebApiSIA.Core.Application.Dtos.Vat;
using WebApiSIA.Core.Domain.Entities;

namespace WebApiSIA.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            //InventoryMovement
            CreateMap<InventoryMovementEntity, InventoryMovementDto>();
            CreateMap<SaveInventoryMovementDto, InventoryMovementEntity>();
            CreateMap<InventoryMovementDto, InventoryMovementEntity>()
                .ForMember(dest => dest.Movement_ID, opt => opt.Ignore());


            //ItemInformationEntity
            CreateMap<ItemInformationEntity, ItemInformationDto>();
                CreateMap<ItemInformationDto, ItemInformationEntity>()
                    .ForMember(dest => dest.ITEM_ID, opt => opt.Ignore());
                CreateMap<SaveItemInformationDto, ItemInformationEntity>()
                    .ForMember(dest => dest.ITEM_ID, opt => opt.Ignore());
                CreateMap<UpdateItemInformationDto, ItemInformationEntity>()
                    .ForMember(dest => dest.ITEM_ID, opt => opt.Ignore());
            CreateMap<ItemInformationEntity, ItemInformationEntity>();


            CreateMap<UserEntity, UserDto>();

            //ItemGroupEntity
            CreateMap<ItemGroupEntity, ItemGruopDto>();


            //VatEntity
            CreateMap<VatEntity, VatDto>();
            CreateMap<SaveVatDto, VatEntity>()
                .ForMember(dest => dest.VAT, opt => opt.MapFrom(src => src.Vat));
            CreateMap<UpdateVatDto, VatEntity>()
                .ForMember(dest => dest.VAT, opt => opt.MapFrom(src => src.Vat));
            CreateMap<VatEntity, UpdateVatDto>()
                .ForMember(dest => dest.Vat, opt => opt.MapFrom(src => src.VAT));
            CreateMap<VatEntity, SaveVatDto>()
                .ForMember(dest => dest.Vat, opt => opt.MapFrom(src => src.VAT));

        }
    }
}