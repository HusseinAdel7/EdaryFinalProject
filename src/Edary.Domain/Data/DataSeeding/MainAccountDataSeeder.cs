using Edary.Entities.MainAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Edary.Domain.Data.DataSeeding.Accounts
{
    public class MainAccountDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<MainAccount, string> _repository;
        private readonly IGuidGenerator _guidGenerator;

        public MainAccountDataSeeder(
            IRepository<MainAccount, string> repository,
            IGuidGenerator guidGenerator)
        {
            _repository = repository;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            // Seed on Host only
            if (context.TenantId != null)
                return;

            var accounts = new List<MainAccountSeedModel>
            {
                // ================= ROOT =================
                new("1", "الأصول", "Assets", null, "الأصول", "Assets", "الميزانية", "Balance Sheet", "All assets"),
                new("2", "الالتزامات", "Liabilities", null, "الالتزامات", "Liabilities", "الميزانية", "Balance Sheet", "All liabilities"),
                new("3", "الحركة التجارية", "Trading Activities", null, "الحركة التجارية", "Trading", "قائمة الدخل", "Income Statement", "Trading activities"),
                new("4", "صافي الربح التجاري", "Net Profit", null, "صافي الربح التجاري", "Net Profit", "قائمة الدخل", "Income Statement", "Net trading profit"),

                // ================= ASSETS =================
                new("11", "الأصول الثابتة", "Fixed Assets", "1", "الأصول الثابتة", "Fixed Assets", "الميزانية", "Balance Sheet", "Fixed assets"),
                new("12", "الأصول المتداولة", "Current Assets", "1", "الأصول المتداولة", "Current Assets", "الميزانية", "Balance Sheet", "Current assets"),
                new("13", "الأموال الجاهزة", "Cash", "1", "الأموال الجاهزة", "Cash", "الميزانية", "Balance Sheet", "Cash accounts"),

                new("121", "العملاء", "Customers", "12", "العملاء", "Customers", "الميزانية", "Balance Sheet", "Customers accounts"),
                new("124", "جاري الشركاء", "Partners Current Account", "12", "جاري الشركاء", "Partners Current", "الميزانية", "Balance Sheet", "Partners current accounts"),

                new("133", "البنوك", "Banks", "13", "البنوك", "Banks", "الميزانية", "Balance Sheet", "Banks accounts"),

                // ================= LIABILITIES =================
                new("21", "الالتزامات الثابتة", "Long-term Liabilities", "2", "الالتزامات الثابتة", "Long-term Liabilities", "الميزانية", "Balance Sheet", "Long-term liabilities"),
                new("22", "الالتزامات المتداولة", "Current Liabilities", "2", "الالتزامات المتداولة", "Current Liabilities", "الميزانية", "Balance Sheet", "Current liabilities"),

                new("211", "رأس المال", "Capital", "21", "رأس المال", "Capital", "الميزانية", "Balance Sheet", "Capital account"),
                new("221", "الذمم الدائنة (الموردون)", "Accounts Payable", "22", "الذمم الدائنة", "Accounts Payable", "الميزانية", "Balance Sheet", "Suppliers"),

                // ================= INCOME =================
                new("32", "صافي المشتريات", "Net Purchases", "3", "صافي المشتريات", "Net Purchases", "قائمة الدخل", "Income Statement", "Net purchases"),
                new("33", "صافي المبيعات", "Net Sales", "3", "صافي المبيعات", "Net Sales", "قائمة الدخل", "Income Statement", "Net sales"),

                // ================= EXPENSES =================
                new("41", "المصاريف العامة", "General Expenses", "4", "المصاريف العامة", "General Expenses", "قائمة الدخل", "Income Statement", "General expenses"),
                new("42", "إيرادات مختلفة غير تجارية", "Other Non-Trading Income", "4", "إيرادات غير تجارية", "Other Income", "قائمة الدخل", "Income Statement", "Other income")
            };

            // AccountNumber => Entity
            var insertedAccounts = new Dictionary<string, MainAccount>();

            // ===== Insert or Load =====
            foreach (var acc in accounts)
            {
                var entity = await _repository.FirstOrDefaultAsync(
                    x => x.AccountNumber == acc.AccountNumber
                );

                if (entity == null)
                {
                    entity = new MainAccount(
                        _guidGenerator.Create().ToString(),
                        acc.AccountNumber
                    )
                    {
                        TenantId = null,
                        AccountName = acc.NameAr,
                        AccountNameEn = acc.NameEn,
                        Title = acc.TitleAr,
                        TitleEn = acc.TitleEn,
                        TransferredTo = acc.TransferToAr,
                        TransferredToEn = acc.TransferToEn,
                        Notes = acc.Notes,
                        IsActive = true
                    };

                    await _repository.InsertAsync(entity, autoSave: true);
                }

                insertedAccounts[acc.AccountNumber] = entity;
            }

            // ===== Parent Linking =====
            foreach (var acc in accounts.Where(a => a.ParentAccountNumber != null))
            {
                var child = insertedAccounts[acc.AccountNumber];
                var parent = insertedAccounts[acc.ParentAccountNumber!];

                if (child.ParentMainAccountId == null)
                {
                    child.ParentMainAccountId = parent.Id;
                    await _repository.UpdateAsync(child, autoSave: true);
                }
            }
        }
    }

    internal record MainAccountSeedModel(
        string AccountNumber,
        string NameAr,
        string NameEn,
        string? ParentAccountNumber,
        string TitleAr,
        string TitleEn,
        string TransferToAr,
        string TransferToEn,
        string Notes
    );
}
