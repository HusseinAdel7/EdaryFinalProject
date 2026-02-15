using Edary.DTOs.TrailBalances;
using Edary.Entities.JournalEntries;
using Edary.Entities.MainAccounts;
using Edary.Entities.SubAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Edary.Services.TrailBalances
{
    public class TrialBalanceManager : DomainService
    {
        private readonly IRepository<MainAccount, string> _mainRepo;
        private readonly IRepository<SubAccount, string> _subRepo;
        private readonly IRepository<JournalEntryDetail, string> _detailRepo;

        public TrialBalanceManager(
            IRepository<MainAccount, string> mainRepo,
            IRepository<SubAccount, string> subRepo,
            IRepository<JournalEntryDetail, string> detailRepo)
        {
            _mainRepo = mainRepo;
            _subRepo = subRepo;
            _detailRepo = detailRepo;
        }

        public async Task<List<TrialBalanceLineDto>> GenerateAsync(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string accountId = null)
        {
            var detailsQuery = await _detailRepo.GetQueryableAsync();

            // تحميل العلاقات
            detailsQuery = detailsQuery
                .Where(d => d.SubAccountId != null && d.JournalEntryId != null);

            // فلترة الحساب
            if (!string.IsNullOrWhiteSpace(accountId))
            {
                var isMain = await _mainRepo.AnyAsync(x => x.Id == accountId);

                if (isMain)
                {
                    var subAccounts = await _subRepo.GetListAsync(x =>
                        x.MainAccountId == accountId);

                    var subIds = subAccounts.Select(x => x.Id).ToList();

                    detailsQuery = detailsQuery
                        .Where(d => subIds.Contains(d.SubAccountId));
                }
                else
                {
                    detailsQuery = detailsQuery
                        .Where(d => d.SubAccountId == accountId);
                }
            }

            // فلترة التاريخ (باستخدام CreationTime)
            if (fromDate.HasValue)
                detailsQuery = detailsQuery
                    .Where(d => d.JournalEntry.CreationTime >= fromDate.Value);

            if (toDate.HasValue)
                detailsQuery = detailsQuery
                    .Where(d => d.JournalEntry.CreationTime <= toDate.Value);

            var list = await AsyncExecuter.ToListAsync(
                detailsQuery.Select(d => new
                {
                    MainId = d.SubAccount.MainAccount.Id,
                    MainName = d.SubAccount.MainAccount.AccountName,
                    SubId = d.SubAccount.Id,
                    SubName = d.SubAccount.AccountName,
                    Debit = d.Debit,
                    Credit = d.Credit
                })
            );

            // =========================
            // تجميع الحسابات الفرعية
            // =========================

            var subTotals = list
                .GroupBy(x => new { x.MainId, x.MainName, x.SubId, x.SubName })
                .Select(g => new
                {
                    g.Key.MainId,
                    g.Key.MainName,
                    g.Key.SubName,
                    TotalDebit = g.Sum(x => x.Debit),
                    TotalCredit = g.Sum(x => x.Credit),
                    NetBalance = g.Sum(x => x.Debit - x.Credit)
                })
                .ToList();

            // =========================
            // تجميع الحسابات الرئيسية
            // =========================

            var mainTotals = subTotals
                .GroupBy(x => new { x.MainId, x.MainName })
                .Select(g => new
                {
                    g.Key.MainName,
                    TotalDebit = g.Sum(x => x.TotalDebit),
                    TotalCredit = g.Sum(x => x.TotalCredit),
                    NetBalance = g.Sum(x => x.NetBalance)
                })
                .ToList();

            var result = new List<TrialBalanceLineDto>();

            foreach (var sub in subTotals)
            {
                result.Add(new TrialBalanceLineDto
                {
                    MainAccountName = sub.MainName,
                    SubAccountName = sub.SubName,
                    TotalDebit = sub.TotalDebit,
                    TotalCredit = sub.TotalCredit,
                    BalanceType = GetBalanceType(sub.NetBalance),
                    BalanceValue = Math.Abs(sub.NetBalance),
                    SortOrder = 1
                });
            }

            foreach (var main in mainTotals)
            {
                result.Add(new TrialBalanceLineDto
                {
                    MainAccountName = main.MainName,
                    SubAccountName = "الإجمالي",
                    TotalDebit = main.TotalDebit,
                    TotalCredit = main.TotalCredit,
                    BalanceType = GetBalanceType(main.NetBalance),
                    BalanceValue = Math.Abs(main.NetBalance),
                    SortOrder = 2
                });
            }

            return result
                .OrderBy(x => x.MainAccountName)
                .ThenBy(x => x.SortOrder)
                .ThenBy(x => x.SubAccountName)
                .ToList();
        }

        private string GetBalanceType(decimal net)
        {
            if (net > 0) return "مدين";
            if (net < 0) return "دائن";
            return "متزن";
        }
    }
}
