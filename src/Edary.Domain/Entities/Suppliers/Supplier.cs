using Edary.Entities.MainAccounts;
using Edary.Entities.SubAccounts;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Edary.Entities.Suppliers
{
    public class Supplier : FullAuditedEntity<string>, IMultiTenant
    {
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string? SubAccountId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string TaxNumber { get; set; }
        public string Notes { get; set; }
        public bool? IsActive { get; set; }
        public string SupplierNameEn { get; set; }

        public SubAccount SubAccount { get; set; }

        public Guid? TenantId { get; set; }
    }
}

