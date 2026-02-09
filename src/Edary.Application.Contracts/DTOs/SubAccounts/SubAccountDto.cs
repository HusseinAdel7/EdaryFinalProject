using System;
using Volo.Abp.Application.Dtos;

namespace Edary.DTOs.SubAccounts
{
    public class SubAccountDto : FullAuditedEntityDto<string>
    {
        public string AccountNumber { get; set; }
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
    }
}
