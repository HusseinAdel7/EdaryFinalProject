using System;
using Volo.Abp.Application.Dtos;

namespace Edary.DTOs.Invoices
{
    public class InvoiceDetailDto : FullAuditedEntityDto<string>
    {
        public string InvoiceId { get; set; }
        public string ItemId { get; set; }
        public string UnitName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TotalBeforeTax { get; set; }
        public decimal? TotalWithTax { get; set; }
        public Guid? TenantId { get; set; }
    }
}

