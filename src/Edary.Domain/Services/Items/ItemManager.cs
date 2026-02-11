using System.Linq;
using System.Threading.Tasks;
using Edary.Entities.Items;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace Edary.Domain.Services.Items
{
    public class ItemManager : DomainService
    {
        private readonly IRepository<Item, string> _itemRepository;

        public ItemManager(IRepository<Item, string> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        /// <summary>
        /// يولّد كود صنف بشكل تسلسلي بصيغة ثابتة مثلاً: ITM-000001, ITM-000002, ...
        /// مع ضمان عدم التكرار (عبر الـ Unique Index على ItemCode).
        /// </summary>
        [UnitOfWork]
        public virtual async Task<string> GenerateNewItemCodeAsync()
        {
            var queryable = await _itemRepository.GetQueryableAsync().ConfigureAwait(false);

            var maxCode = queryable
                .Select(i => i.ItemCode)
                .OrderByDescending(c => c)
                .FirstOrDefault();

            // أول كود في النظام
            if (string.IsNullOrWhiteSpace(maxCode))
            {
                return "ITM-000001";
            }

            // نستخرج الجزء الرقمي من آخر كود (يدعم وجود Prefix قديم)
            var numericPart = new string(maxCode.Where(char.IsDigit).ToArray());

            if (!long.TryParse(numericPart, out var maxValue))
            {
                throw new BusinessException("Edary:InvalidItemCodeFormat",
                    $"Cannot generate new ItemCode because existing max code '{maxCode}' does not contain a valid numeric part.");
            }

            var nextValue = maxValue + 1;

            // نثبّت طول الجزء الرقمي على 6 أرقام
            return $"ITM-{nextValue:D6}";
        }
    }
}

