using Edary.Entities.JournalEntries;
using Edary.Entities.Suppliers;
using Edary.Entities.Warehouses;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Edary.Entities.Invoices
{
    public class Invoice : FullAuditedEntity<string>, IMultiTenant
    {
        public string InvoiceNumber { get; set; }
        public string InvoiceType { get; set; }
        public string? SupplierId { get; set; }
        public string WarehouseId { get; set; }
        public string Currency { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetAmount { get; private set; } // Computed property
        public decimal? TaxAmount { get; set; }
        public decimal? GrandTotal { get; private set; } // Computed property
        public string? JournalEntryId { get; set; }
        public string PaymentStatus { get; set; }
        public string Notes { get; set; }

        public Supplier Supplier { get; set; }
        public Warehouse Warehouse { get; set; }
        public JournalEntry JournalEntry { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }

        public Guid? TenantId { get; set; }

        public Invoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }
    }
}

