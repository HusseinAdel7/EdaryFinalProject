using Edary.Entities.Items;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Edary.Entities.Invoices
{
    public class InvoiceDetail : FullAuditedEntity<string>, IMultiTenant
    {
        public string InvoiceId { get; set; }
        public string ItemId { get; set; }
        public string UnitName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal? TaxAmount { get; private set; } // Computed property
        public decimal? TotalBeforeTax { get; private set; } // Computed property
        public decimal? TotalWithTax { get; private set; } // Computed property

        public Invoice Invoice { get; set; }

        // Navigation properties
        public Item Item { get; set; }

        public Guid? TenantId { get; set; }
    }
}

