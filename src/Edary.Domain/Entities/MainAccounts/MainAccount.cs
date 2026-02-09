using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Edary.Entities.MainAccounts
{
    public class MainAccount : FullAuditedEntity<string>, IMultiTenant
    {
        protected MainAccount() { }

        public MainAccount(string id, string accountNumber)
        {
            Id = id;
            AccountNumber = accountNumber;
        }

        public string AccountNumber { get; private set; }
        public string AccountName { get; set; }
        public string? AccountNameEn { get; set; }

        public string? Title { get; set; }
        public string? TitleEn { get; set; }

        public string? TransferredTo { get; set; }
        public string? TransferredToEn { get; set; }

        public bool IsActive { get; set; }
        public string? Notes { get; set; }



        public string? ParentMainAccountId { get; set; }

        public MainAccount? ParentMainAccount { get; set; }

        public Guid? TenantId { get; set; }
    }
}