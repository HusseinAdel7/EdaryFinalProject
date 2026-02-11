using System;
using Volo.Abp.Application.Dtos;

namespace Edary.DTOs.Items
{
    public class ItemPriceDto : FullAuditedEntityDto<string>
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
        public Guid? TenantId { get; set; }
    }
}

