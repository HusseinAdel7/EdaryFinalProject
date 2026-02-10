using System.Linq;
using System.Threading.Tasks;
using Edary.Entities.Suppliers;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace Edary.Domain.Services.Suppliers
{
    public class SupplierManager : DomainService
    {
        private readonly IRepository<Supplier, string> _supplierRepository;

        public SupplierManager(IRepository<Supplier, string> supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        /// <summary>
        /// يولّد كود مورد بشكل تسلسلي بصيغة ثابتة مثلاً: SUP-000001, SUP-000002, ...
        /// مع ضمان عدم التكرار (عبر الـ Unique Index على SupplierCode).
        /// </summary>
        [UnitOfWork]
        public virtual async Task<string> GenerateNewSupplierCodeAsync()
        {
            var queryable = await _supplierRepository.GetQueryableAsync().ConfigureAwait(false);

            var maxCode = queryable
                .Select(s => s.SupplierCode)
                .OrderByDescending(c => c)
                .FirstOrDefault();

            // أول كود في النظام
            if (string.IsNullOrWhiteSpace(maxCode))
            {
                return "SUP-000001";
            }

            // نستخرج الجزء الرقمي من آخر كود (يدعم وجود Prefix قديم)
            var numericPart = new string(maxCode.Where(char.IsDigit).ToArray());

            if (!long.TryParse(numericPart, out var maxValue))
            {
                throw new BusinessException("Edary:InvalidSupplierCodeFormat",
                    $"Cannot generate new SupplierCode because existing max code '{maxCode}' does not contain a valid numeric part.");
            }

            var nextValue = maxValue + 1;

            // نثبّت طول الجزء الرقمي على 6 أرقام (تقدر تغيّره لو حابب)
            return $"SUP-{nextValue:D6}";
        }
    }
}

