using Volo.Abp.Application.Dtos;

namespace Edary.DTOs.SubAccounts
{
    public class SubAccountPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
        public bool? IsActive { get; set; }
        public string? MainAccountId { get; set; }
    }
}
