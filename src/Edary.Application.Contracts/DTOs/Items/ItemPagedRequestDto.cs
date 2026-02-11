using Volo.Abp.Application.Dtos;

namespace Edary.DTOs.Items
{
    public class ItemPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
        public bool? IsActive { get; set; }
        public string? GroupName { get; set; }
        public string? ItemType { get; set; }
    }
}

