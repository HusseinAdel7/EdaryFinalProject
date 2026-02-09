using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Edary.DTOs.MainAccounts
{
    // DTO for list/grid views (lighter version)
    public class MainAccountListDto : EntityDto<string>
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string? AccountNameEn { get; set; }
        public string? Title { get; set; }
        public bool? IsActive { get; set; }
        public string? ParentMainAccountId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
