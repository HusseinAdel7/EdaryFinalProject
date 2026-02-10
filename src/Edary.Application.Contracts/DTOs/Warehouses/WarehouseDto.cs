using System;
using Volo.Abp.Application.Dtos;

namespace Edary.DTOs.Warehouses
{
    public class WarehouseDto : FullAuditedEntityDto<string>
    {
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string Location { get; set; }
        public string ManagerName { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public string WarehouseNameEn { get; set; }
        public string ManagerNameEn { get; set; }
        public Guid? TenantId { get; set; }
    }
}

