using Volo.Abp.Application.Dtos;

namespace Edary.DTOs.Warehouses
{
    public class WarehousePagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
        public bool? IsActive { get; set; }
    }
}

