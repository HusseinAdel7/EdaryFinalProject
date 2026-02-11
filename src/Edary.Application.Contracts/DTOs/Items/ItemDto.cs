using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Edary.DTOs.Items
{
    public class ItemDto : FullAuditedEntityDto<string>
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public string GroupName { get; set; }
        public string Barcode { get; set; }
        public decimal OpeningPrice { get; set; }
        public decimal? MinLimit { get; set; }
        public decimal? MaxLimit { get; set; }
        public decimal? ReorderQty { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Notes { get; set; }
        public bool? IsActive { get; set; }
        public string ItemNameEn { get; set; }
        public string ItemTypeEn { get; set; }
        public string GroupNameEn { get; set; }
        public string UnitOfMeasureEn { get; set; }
        public Guid? TenantId { get; set; }

        public ICollection<ItemPriceDto> ItemPrices { get; set; }
    }
}

