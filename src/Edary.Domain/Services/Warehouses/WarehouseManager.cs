using System.Linq;
using System.Threading.Tasks;
using Edary.Entities.Warehouses;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace Edary.Domain.Services.Warehouses
{
    public class WarehouseManager : DomainService
    {
        private readonly IRepository<Warehouse, string> _warehouseRepository;

        public WarehouseManager(IRepository<Warehouse, string> warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        /// <summary>
        /// يولّد كود مخزن بشكل تسلسلي بصيغة ثابتة مثلاً: WH-000001, WH-000002, ...
        /// مع ضمان عدم التكرار (عبر الـ Unique Index على WarehouseCode).
        /// </summary>
        [UnitOfWork]
        public virtual async Task<string> GenerateNewWarehouseCodeAsync()
        {
            var queryable = await _warehouseRepository.GetQueryableAsync().ConfigureAwait(false);

            var maxCode = queryable
                .Select(w => w.WarehouseCode)
                .OrderByDescending(c => c)
                .FirstOrDefault();

            // أول كود في النظام
            if (string.IsNullOrWhiteSpace(maxCode))
            {
                return "WH-000001";
            }

            // نستخرج الجزء الرقمي من آخر كود (يدعم وجود Prefix قديم)
            var numericPart = new string(maxCode.Where(char.IsDigit).ToArray());

            if (!long.TryParse(numericPart, out var maxValue))
            {
                throw new BusinessException("Edary:InvalidWarehouseCodeFormat",
                    $"Cannot generate new WarehouseCode because existing max code '{maxCode}' does not contain a valid numeric part.");
            }

            var nextValue = maxValue + 1;

            // نثبّت طول الجزء الرقمي على 6 أرقام
            return $"WH-{nextValue:D6}";
        }
    }
}

