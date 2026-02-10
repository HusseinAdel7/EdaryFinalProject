using System.ComponentModel.DataAnnotations;
using Edary.Consts.Warehouses;

namespace Edary.DTOs.Warehouses
{
    public class CreateWarehouseDto
    {
        [Required]
        [StringLength(WarehouseConsts.MaxWarehouseNameLength)]
        public string WarehouseName { get; set; }

        [StringLength(WarehouseConsts.MaxLocationLength)]
        public string Location { get; set; }

        [StringLength(WarehouseConsts.MaxManagerNameLength)]
        public string ManagerName { get; set; }

        [StringLength(WarehouseConsts.MaxNotesLength)]
        public string Notes { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(WarehouseConsts.MaxWarehouseNameEnLength)]
        public string WarehouseNameEn { get; set; }

        [StringLength(WarehouseConsts.MaxManagerNameEnLength)]
        public string ManagerNameEn { get; set; }
    }
}

