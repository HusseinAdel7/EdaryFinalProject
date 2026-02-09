using Edary.Entities.MainAccounts;
using Edary.Entities.SubAccounts;
using Microsoft.Extensions.Logging;
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
    public class SubAccountDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<SubAccount, string> _repository;
        private readonly IRepository<MainAccount, string> _mainAccountRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ILogger<SubAccountDataSeeder> _logger;

        public SubAccountDataSeeder(
            IRepository<SubAccount, string> repository,
            IRepository<MainAccount, string> mainAccountRepository,
            IGuidGenerator guidGenerator,
            ILogger<SubAccountDataSeeder> logger)
        {
            _repository = repository;
            _mainAccountRepository = mainAccountRepository;
            _guidGenerator = guidGenerator;
            _logger = logger;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (context.TenantId != null)
            {
                _logger.LogInformation("Skipping SubAccount seeding for tenant context");
                return;
            }

            _logger.LogInformation("Starting SubAccount seeding...");

            try
            {
                // تحميل الـ MainAccounts
                var mainAccounts = (await _mainAccountRepository.GetListAsync())
                                    .ToDictionary(x => x.AccountNumber, x => x.Id);

                if (!mainAccounts.Any())
                {
                    _logger.LogWarning("No MainAccounts found. Please seed MainAccounts first!");
                    return;
                }

                var subAccounts = GetSubAccountsData();
                var entitiesToInsert = new List<SubAccount>();
                var skippedCount = 0;
                var errorCount = 0;

                foreach (var sa in subAccounts)
                {
                    try
                    {
                        // التحقق من وجود MainAccount
                        if (!mainAccounts.ContainsKey(sa.MainAccountNumber))
                        {
                            _logger.LogWarning(
                                "MainAccount '{MainAccountNumber}' not found for SubAccount '{AccountNumber}'",
                                sa.MainAccountNumber, sa.AccountNumber);
                            errorCount++;
                            continue;
                        }

                        // التحقق من عدم وجود SubAccount مسبقاً
                        var existing = await _repository.FirstOrDefaultAsync(x => x.AccountNumber == sa.AccountNumber);
                        if (existing != null)
                        {
                            skippedCount++;
                            continue;
                        }

                        var mainAccountId = mainAccounts[sa.MainAccountNumber];

                        var entity = new SubAccount(_guidGenerator.Create().ToString(), sa.AccountNumber)
                        {
                            AccountName = sa.AccountName,
                            AccountNameEn = sa.AccountNameEn,
                            MainAccountId = mainAccountId,
                            Title = sa.Title,
                            TitleEn = sa.TitleEn,
                            AccountType = sa.AccountType,
                            AccountTypeEn = sa.AccountTypeEn,
                            CreditAmount = sa.CreditAmount,
                            StandardCreditRate = sa.StandardCreditRate,
                            Commission = sa.Commission,
                            Percentage = sa.Percentage,
                            AccountCurrency = sa.AccountCurrency,
                            AccountCurrencyEn = sa.AccountCurrencyEn,
                            Notes = sa.Notes,
                            IsActive = true,
                            TenantId = null
                        };

                        entitiesToInsert.Add(entity);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing SubAccount '{AccountNumber}'", sa.AccountNumber);
                        errorCount++;
                    }
                }

                // Bulk Insert
                if (entitiesToInsert.Any())
                {
                    await _repository.InsertManyAsync(entitiesToInsert, autoSave: true);
                    _logger.LogInformation(
                        "SubAccount seeding completed successfully. Inserted: {InsertedCount}, Skipped: {SkippedCount}, Errors: {ErrorCount}",
                        entitiesToInsert.Count, skippedCount, errorCount);
                }
                else
                {
                    _logger.LogInformation("No new SubAccounts to seed. All accounts already exist.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatal error during SubAccount seeding");
                throw;
            }
        }

        private List<SubAccountSeedModel> GetSubAccountsData()
        {
            return new List<SubAccountSeedModel>
            {
                // ===== الأصول الثابتة (MainAccount AccountNumber: 11) =====
                new("111", "العقارات", "Real Estate", "11", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب العقارات"),
                new("112", "الأثاث", "Furniture", "11", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب الأثاث"),
                new("113", "السيارات", "Vehicles", "11", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب السيارات"),
                new("114", "مصاريف التأسيس", "Formation Expenses", "11", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب مصاريف التأسيس"),
                new("115", "التجهيزات المكتبية", "Office Equipment", "11", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب التجهيزات المكتبية"),
                new("116", "الأراضي", "Land", "11", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب الأراضي"),
                new("117", "أجهزة الكمبيوتر", "Computers", "11", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب أجهزة الكمبيوتر"),
                new("118", "أجهزة كهربائية", "Electrical Appliances", "11", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب أجهزة كهربائية"),
                new("119", "المفروشات", "Furnishings", "11", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب المفروشات"),

                // ===== العملاء (MainAccount AccountNumber: 121) =====
                new("12101", "زبون رقم 1", "Customer 1", "121", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب زبون رقم 1"),
                new("12102", "زبون رقم 2", "Customer 2", "121", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب زبون رقم 2"),
                new("12103", "كنوز", "Kunooz", "121", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب كنوز"),
                new("12104", "مارينا", "Marina", "121", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب مارينا"),
                new("12105", "الفهد", "Al-Fahd", "121", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب الفهد"),
                new("12106", "الصعيدي", "Al-Saidi", "121", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب الصعيدي"),
                new("12107", "الخطيب", "Al-Khatib", "121", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب الخطيب"),
                new("12108", "الإسراء", "Al-Israa", "121", "الميزانية", "Balance Sheet", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب الإسراء"),

                // ===== الأصول المتداولة (MainAccount AccountNumber: 12) =====
                new("122", "أوراق القبض", "Notes Receivable", "12", "الميزانية", "Balance Sheet", "غير محدد", "N/A", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", ""),
                new("123", "الإجازات والطلبيات", "Orders & Requests", "12", "الميزانية", "Balance Sheet", "غير محدد", "N/A", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", ""),
                new("125", "شيكات تحت التحصيل", "Checks Under Collection", "12", "الميزانية", "Balance Sheet", "غير محدد", "N/A", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", ""),
                new("126", "تأمين لدي الغير", "Insurance with Others", "12", "الميزانية", "Balance Sheet", "غير محدد", "N/A", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", ""),
                new("127", "أوراق مدفوعة مقدماً", "Prepaid Documents", "12", "الميزانية", "Balance Sheet", "غير محدد", "N/A", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", ""),

                // ===== جاري الشركاء (MainAccount AccountNumber: 124) =====
                new("1241", "جاري احمد", "Ahmed Current", "124", "الميزانية", "Balance Sheet", "غير محدد", "N/A", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", ""),
                new("1242", "ج محمد", "Mohamed Current", "124", "الميزانية", "Balance Sheet", "غير محدد", "N/A", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", ""),
                new("1243", "ج عبدالرحمن", "Abdulrahman Current", "124", "الميزانية", "Balance Sheet", "غير محدد", "N/A", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", ""),

                // ===== الأموال الجاهزة (MainAccount AccountNumber: 13) =====
                new("131", "الصندوق", "Cash Box", "13", "الميزانية", "Balance Sheet", "غير محدد", "N/A", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", ""),
                new("132", "الصندوق الإحتياطي", "Reserve Cash", "13", "الميزانية", "Balance Sheet", "غير محدد", "N/A", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", ""),

                // ===== البنوك (MainAccount AccountNumber: 133) =====
                new("1331", "مصرف رقم 1", "Bank 1", "133", "الميزانية", "Balance Sheet", "غير محدد", "N/A", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", ""),
                new("1332", "بنك مصر", "Bank Misr", "133", "الميزانية", "Balance Sheet", "غير محدد", "N/A", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", ""),
                new("1333", "بنك القاهرة", "Cairo Bank", "133", "الميزانية", "Balance Sheet", "غير محدد", "N/A", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", ""),
                new("1334", "بنك إسكندرية", "Alexandria Bank", "133", "الميزانية", "Balance Sheet", "غير محدد", "N/A", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", ""),
                new("1335", "البنك الأهلي", "National Bank of Egypt", "133", "الميزانية", "Balance Sheet", "غير محدد", "N/A", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", ""),

                // ===== رأس المال (MainAccount AccountNumber: 211) =====
                new("2111", "رأسمال الشريك 1", "Partner 1 Capital", "211", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حصة الشريك الأول في رأس المال"),
                new("2112", "رأسمال الشريك 2", "Partner 2 Capital", "211", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حصة الشريك الثاني في رأس المال"),
                new("2113", "رأس مال احمد", "Ahmed Capital", "211", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "رأس مال أحمد"),
                new("2114", "رأس مال عبدالرحمن", "Abdulrahman Capital", "211", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "رأس مال عبدالرحمن"),
                new("2115", "رأس مال اسلام", "Islam Capital", "211", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "رأس مال إسلام"),
                new("2116", "رأس مال محمد", "Mohamed Capital", "211", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "رأس مال محمد"),

                // ===== الالتزامات الثابتة (MainAccount AccountNumber: 21) =====
                new("212", "القروض الدائنة", "Loans Payable", "21", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "قروض مستحقة الدفع"),
                new("213", "أموال قيد التشغيل", "Funds in Process", "21", "الميزانية", "Balance Sheet", "غير محدد", "N/A", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "رؤوس أموال جارية لمشروعات تحت التنفيذ"),

                // ===== الذمم الدائنة - الموردون (MainAccount AccountNumber: 221) =====
                new("22101", "المورد رقم 1", "Supplier 1", "221", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب مورد رقم 1"),
                new("22102", "المورد رقم 2", "Supplier 2", "221", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب مورد رقم 2"),
                new("22103", "فريش", "Fresh", "221", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "مورد فريش"),
                new("22104", "يونيفرسال", "Universal", "221", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "مورد يونيفرسال"),
                new("22105", "إيديال", "Ideal", "221", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "مورد إيديال"),

                // ===== الالتزامات المتداولة (MainAccount AccountNumber: 22) =====
                new("222", "أوراق الدفع", "Notes Payable", "22", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب أوراق الدفع"),
                new("223", "حسابات دائنة قيد الدفع", "Accounts Payable", "22", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب حسابات دائنة قيد الدفع"),
                new("224", "مصروفات مستحقة", "Accrued Expenses", "22", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب مصروفات مستحقة"),

                // ===== مخصص الاستهلاك (MainAccount AccountNumber: 23) =====
                new("231", "مخصص اهتلاك العقارات", "Real Estate Depreciation", "23", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "مخصص اهتلاك العقارات"),
                new("232", "مخصص اهتلاك الأثاث", "Furniture Depreciation", "23", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "مخصص اهتلاك الأثاث"),
                new("233", "مخصص اهتلاك السيارات", "Vehicles Depreciation", "23", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "مخصص اهتلاك السيارات"),
                new("234", "مخصص اهتلاك مصاريف التأسيس", "Formation Expenses Amortization", "23", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "مخصص اهتلاك مصاريف التأسيس"),
                new("235", "مخصص اهتلاك التجهيزات المكتبية", "Office Equipment Depreciation", "23", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "مخصص اهتلاك التجهيزات المكتبية"),
                new("236", "مجمع الإهلاك", "Accumulated Depreciation", "23", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "مجمع الإهلاك"),

                // ===== أرباح/خسائر مدورة (MainAccount AccountNumber: 24) =====
                new("241", "أرباح سنوات سابقة", "Retained Earnings - Prior Years", "24", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب أرباح سنوات سابقة"),
                new("242", "أرباح السنة الحالية", "Current Year Profit", "24", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب أرباح السنة الحالية"),

                // ===== المؤونات (MainAccount AccountNumber: 25) =====
                new("251", "مؤونة ديون مشكوك فيها", "Allowance for Doubtful Debts", "25", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب مؤونة ديون مشكوك فيها"),
                new("252", "مؤونة حوادث طارئة", "Emergency Provision", "25", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب مؤونة حوادث طارئة"),
                new("253", "مؤونة حملات دعائية", "Advertising Campaign Provision", "25", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب مؤونة حملات دعائية"),
                new("254", "مؤونة حسابات قيد التحصيل", "Collection Accounts Provision", "25", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب مؤونة حسابات قيد التحصيل"),

                // ===== الاحتياطي (MainAccount AccountNumber: 26) =====
                new("261", "الإحتياطي الإجباري", "Mandatory Reserve", "26", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب الإحتياطي الإجباري"),
                new("262", "إحتياطي خسائر متوقعة", "Expected Loss Reserve", "26", "الميزانية", "Balance Sheet", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب إحتياطي خسائر متوقعة"),

                // ===============================================
                // ===== قائمة الدخل =====
                // ===============================================

                // ===== الحركة التجارية (MainAccount AccountNumber: 3) =====
                new("31", "بضاعة جاهزة أول المدة", "Beginning Inventory", "3", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب بضاعة جاهزة أول المدة"),
                new("34", "الحسم التجاري الممنوح", "Trade Discount Granted", "3", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب الحسم التجاري الممنوح"),
                new("35", "الحسم التجاري المكتسب", "Trade Discount Earned", "3", "قائمة الدخل", "Income Statement", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب الحسم التجاري المكتسب"),

                // ===== صافي المشتريات (MainAccount AccountNumber: 32) =====
                new("321", "المشتريات", "Purchases", "32", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب المشتريات"),
                new("322", "مردودات المشتريات", "Purchase Returns", "32", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب مردودات المشتريات"),

                // ===== صافي المبيعات (MainAccount AccountNumber: 33) =====
                new("331", "المبيعات", "Sales", "33", "قائمة الدخل", "Income Statement", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب المبيعات"),
                new("332", "مردودات المبيعات", "Sales Returns", "33", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب مردودات المبيعات"),

                // ===== المصروفات العامة (MainAccount AccountNumber: 41) =====
                new("4101", "أجور العمال", "Labor Wages", "41", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب أجور العمال"),
                new("4102", "ضرائب ورسوم", "Taxes and Fees", "41", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب ضرائب ورسوم"),
                new("4103", "مصاريف نثرية", "Miscellaneous Expenses", "41", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب مصاريف نثرية"),
                new("4104", "مصاريف الضيافة", "Hospitality Expenses", "41", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب مصاريف الضيافة"),
                new("4105", "مصاريف شخصية", "Personal Expenses", "41", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب مصاريف شخصية"),
                new("4106", "الماء", "Water", "41", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب الماء"),
                new("4107", "الكهرباء", "Electricity", "41", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب الكهرباء"),
                new("4108", "الهاتف", "Telephone", "41", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب الهاتف"),
                new("4109", "اشتراكات سنوية", "Annual Subscriptions", "41", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب اشتراكات سنوية"),
                new("4110", "عدد و أدوات مستهلكة", "Consumable Tools", "41", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب عدد و أدوات مستهلكة"),
                new("4111", "خسائر أخرى", "Other Losses", "41", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب خسائر أخرى"),
                new("4112", "الضرائب", "Taxes", "41", "قائمة الدخل", "Income Statement", "مدين دائما", "Always Debit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب الضرائب"),

                // ===== إيرادات مختلفة غير تجارية (MainAccount AccountNumber: 42) =====
                new("421", "فوائد مصرفية", "Bank Interest", "42", "قائمة الدخل", "Income Statement", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب فوائد مصرفية"),
                new("422", "أرباح أعمال غير تجارية", "Non-Trading Income", "42", "قائمة الدخل", "Income Statement", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب أرباح أعمال غير تجارية"),
                new("423", "عمولات وكمسيونات", "Commissions", "42", "قائمة الدخل", "Income Statement", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب عمولات وكمسيونات"),
                new("424", "أرباح تشغيل أموال", "Investment Income", "42", "قائمة الدخل", "Income Statement", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب أرباح تشغيل أموال"),
                new("425", "إيرادات أموال احتياطية (تأمين)", "Insurance Reserve Income", "42", "قائمة الدخل", "Income Statement", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب إيرادات أموال احتياطية (تأمين)"),
                new("426", "إيرادات أخرى", "Other Revenues", "42", "قائمة الدخل", "Income Statement", "دائن دائما", "Always Credit", 0, "غير محدد", 0, 0, "جنيه مصري", "EGP", "حساب إيرادات أخرى"),
            };
        }
    }

    internal record SubAccountSeedModel(
        string AccountNumber,
        string AccountName,
        string AccountNameEn,
        string MainAccountNumber,
        string Title,
        string TitleEn,
        string AccountType,
        string AccountTypeEn,
        decimal? CreditAmount,
        string StandardCreditRate,
        decimal? Commission,
        decimal? Percentage,
        string AccountCurrency,
        string AccountCurrencyEn,
        string Notes
    );
}