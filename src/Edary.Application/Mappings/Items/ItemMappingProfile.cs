using AutoMapper;
using Edary.DTOs.Items;
using Edary.Entities.Items;

namespace Edary.Application.Mappings.Items
{
    public class ItemMappingProfile : Profile
    {
        public ItemMappingProfile()
        {
            // Create
            CreateMap<CreateItemDto, Item>();

            // 🔥 أهم تعديل هنا
            CreateMap<UpdateItemDto, Item>()
                .ForMember(dest => dest.ItemPrices, opt => opt.Ignore());

            CreateMap<CreateItemPriceDto, ItemPrice>();
            CreateMap<UpdateItemPriceDto, ItemPrice>();

            // Entity -> DTO
            CreateMap<Item, ItemDto>();
            CreateMap<ItemPrice, ItemPriceDto>();
        }
    }
}

