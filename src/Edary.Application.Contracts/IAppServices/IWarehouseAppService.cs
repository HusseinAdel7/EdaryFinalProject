using Edary.DTOs.Warehouses;
using Volo.Abp.Application.Services;

namespace Edary.IAppServices
{
    public interface IWarehouseAppService :
        ICrudAppService<
            WarehouseDto,
            string,
            WarehousePagedRequestDto,
            CreateWarehouseDto,
            UpdateWarehouseDto>
    {
    }
}

