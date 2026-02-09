using System.ComponentModel.DataAnnotations;

namespace Edary.DTOs.SubAccounts
{
    public class CreateSubAccountDto
    {
        [Required]
        public string AccountName { get; set; }
        [Required]
        public string MainAccountId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string AccountType { get; set; }
        public decimal? CreditAmount { get; set; }
        [Required]
        public string StandardCreditRate { get; set; }
        public decimal? Commission { get; set; }
        public decimal? Percentage { get; set; }
        [Required]
        public string AccountCurrency { get; set; }
        public string Notes { get; set; }
        public bool? IsActive { get; set; } = true;
        public string? AccountNameEn { get; set; }
        public string? TitleEn { get; set; }
        public string? AccountTypeEn { get; set; }
        public string? AccountCurrencyEn { get; set; }
    }
}
