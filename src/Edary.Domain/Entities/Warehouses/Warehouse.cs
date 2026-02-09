using Edary.Entities.SubAccounts;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Edary.Entities.Warehouses
{
    public class Warehouse : FullAuditedEntity<string>, IMultiTenant
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

