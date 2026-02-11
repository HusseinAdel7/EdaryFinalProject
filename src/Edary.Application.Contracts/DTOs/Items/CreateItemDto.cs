using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Edary.Consts.Items;

namespace Edary.DTOs.Items
{
    public class CreateItemDto
    {
        [Required]
        [StringLength(ItemConsts.MaxItemNameLength)]
        public string ItemName { get; set; }

        [StringLength(ItemConsts.MaxItemTypeLength)]
        public string ItemType { get; set; }

        [StringLength(ItemConsts.MaxGroupNameLength)]
        public string GroupName { get; set; }

        [StringLength(ItemConsts.MaxBarcodeLength)]
        public string Barcode { get; set; }

        [Required]
        public decimal OpeningPrice { get; set; }

        public decimal? MinLimit { get; set; }
        public decimal? MaxLimit { get; set; }
        public decimal? ReorderQty { get; set; }

        [StringLength(ItemConsts.MaxUnitOfMeasureLength)]
        public string UnitOfMeasure { get; set; }

        [StringLength(ItemConsts.MaxNotesLength)]
        public string Notes { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(ItemConsts.MaxItemNameEnLength)]
        public string ItemNameEn { get; set; }

        [StringLength(ItemConsts.MaxItemTypeEnLength)]
        public string ItemTypeEn { get; set; }

        [StringLength(ItemConsts.MaxGroupNameEnLength)]
        public string GroupNameEn { get; set; }

        [StringLength(ItemConsts.MaxUnitOfMeasureEnLength)]
        public string UnitOfMeasureEn { get; set; }

        [MinLength(1, ErrorMessage = "An Item must have at least one price.")]
        public ICollection<CreateItemPriceDto> ItemPrices { get; set; }

        public CreateItemDto()
        {
            ItemPrices = new HashSet<CreateItemPriceDto>();
        }
    }
}

