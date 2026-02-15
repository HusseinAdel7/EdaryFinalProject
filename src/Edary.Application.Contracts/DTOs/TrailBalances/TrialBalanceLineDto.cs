using System;
using System.Collections.Generic;
using System.Text;

namespace Edary.DTOs.TrailBalances
{
    public class TrialBalanceLineDto
    {
        public string MainAccountName { get; set; }
        public string SubAccountName { get; set; }

        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }

        public string BalanceType { get; set; }
        public decimal BalanceValue { get; set; }

        public int SortOrder { get; set; }
    }

}
