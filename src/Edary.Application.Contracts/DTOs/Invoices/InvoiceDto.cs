using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Edary.DTOs.Invoices
{
    public class InvoiceDto : FullAuditedEntityDto<string>
    {
        public string InvoiceNumber { get; set; }
        public string InvoiceType { get; set; }
        public string? SupplierId { get; set; }
        public string WarehouseId { get; set; }
        public string Currency { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? GrandTotal { get; set; }
        public string? JournalEntryId { get; set; }
        public string PaymentStatus { get; set; }
        public string Notes { get; set; }
        public Guid? TenantId { get; set; }

        public ICollection<InvoiceDetailDto> InvoiceDetails { get; set; }
    }
}

