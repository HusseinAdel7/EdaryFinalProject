using Edary.Entities.MainAccounts;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Edary.Entities.SubAccounts
{
    public class SubAccount : FullAuditedEntity<string>, IMultiTenant
    {
        protected SubAccount() { }

        public SubAccount(string id, string accountNumber)
        {
            Id = id;
            AccountNumber = accountNumber;
        }

        public string AccountNumber { get; private set; }
        public string AccountName { get; set; }
        public string? MainAccountId { get; set; }
        public string Title { get; set; }
        public string AccountType { get; set; }
        public decimal? CreditAmount { get; set; }
        public string StandardCreditRate { get; set; }
        public decimal? Commission { get; set; }
        public decimal? Percentage { get; set; }
        public string AccountCurrency { get; set; }
        public string Notes { get; set; }
        public bool? IsActive { get; set; }
        public string AccountNameEn { get; set; }
        public string TitleEn { get; set; }
        public string AccountTypeEn { get; set; }
        public string AccountCurrencyEn { get; set; }
        public Guid? TenantId { get; set; }

        public MainAccount? MainAccount { get; set; } 
    }
}
