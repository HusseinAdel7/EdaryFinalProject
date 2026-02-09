using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Edary.Entities.JournalEntries
{
    public class JournalEntry : FullAuditedEntity<string>, IMultiTenant
    {
        public string Currency { get; set; }
        public decimal ExchangeRate { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public string CurrencyEn { get; set; }

        public ICollection<JournalEntryDetail> JournalEntryDetails { get; set; }

        public Guid? TenantId { get; set; }

        public JournalEntry()
        {
            JournalEntryDetails = new HashSet<JournalEntryDetail>();
        }
    }
}

