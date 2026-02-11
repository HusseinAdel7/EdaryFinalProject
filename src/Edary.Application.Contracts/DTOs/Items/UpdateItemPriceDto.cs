using System;
using System.ComponentModel.DataAnnotations;
using Edary.Consts.Items;

namespace Edary.DTOs.Items
{
    public class UpdateItemPriceDto
    {
        public string Id { get; set; }

        [Required]
        [StringLength(ItemPriceConsts.MaxUnitNameLength)]
        public string UnitName { get; set; }

        public decimal? WholePrice { get; set; }
        public decimal? RetailPrice { get; set; }
        public decimal? ConsumerPrice { get; set; }

        [StringLength(ItemPriceConsts.MaxCurrencyLength)]
        public string Currency { get; set; }

        public DateTime? EffectiveDate { get; set; }
        public bool? IsActive { get; set; }

        [StringLength(ItemPriceConsts.MaxUnitNameEnLength)]
        public string UnitNameEn { get; set; }

        [StringLength(ItemPriceConsts.MaxCurrencyEnLength)]
        public string CurrencyEn { get; set; }
    }
}

