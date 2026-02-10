using AutoMapper;
using Edary.DTOs.Warehouses;
using Edary.Entities.Warehouses;

namespace Edary.Application.Mappings.Warehouses
{
    public class WarehouseMappingProfile : Profile
    {
        public WarehouseMappingProfile()
        {
            CreateMap<Warehouse, WarehouseDto>();
            CreateMap<CreateWarehouseDto, Warehouse>();
            CreateMap<UpdateWarehouseDto, Warehouse>();
        }
    }
}

