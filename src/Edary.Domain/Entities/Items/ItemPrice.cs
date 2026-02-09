using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Edary.Entities.Items
{
    public class ItemPrice : FullAuditedEntity<string>, IMultiTenant
    {
        public string ItemId { get; set; }
        public string UnitName { get; set; }
        public decimal? WholePrice { get; set; }
        public decimal? RetailPrice { get; set; }
        public decimal? ConsumerPrice { get; set; }
        public string Currency { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public bool? IsActive { get; set; }
        public string UnitNameEn { get; set; }
        public string CurrencyEn { get; set; }

        public Item Item { get; set; }

        public Guid? TenantId { get; set; }
    }
}

