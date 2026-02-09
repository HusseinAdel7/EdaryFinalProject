using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Edary.DTOs.MainAccounts
{
    public class MainAccountDto : FullAuditedEntityDto<string>
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string? AccountNameEn { get; set; }

        public string? Title { get; set; }
        public string? TitleEn { get; set; }

        public string? TransferredTo { get; set; }
        public string? TransferredToEn { get; set; }

        public bool IsActive { get; set; }
        public string? Notes { get; set; }

        public string? ParentMainAccountId { get; set; }
        public Guid? TenantId { get; set; }

    }
}
