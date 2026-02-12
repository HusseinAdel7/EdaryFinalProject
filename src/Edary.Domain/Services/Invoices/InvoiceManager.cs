using System.Linq;
using System.Threading.Tasks;
using Edary.Entities.Invoices;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace Edary.Domain.Services.Invoices
{
    public class InvoiceManager : DomainService
    {
        private readonly IRepository<Invoice, string> _invoiceRepository;

        public InvoiceManager(IRepository<Invoice, string> invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        /// <summary>
        /// يولّد رقم فاتورة بشكل تسلسلي بصيغة ثابتة مثلاً: INV-000001, INV-000002, ...
        /// مع ضمان عدم التكرار (عبر الـ Unique Index على InvoiceNumber).
        /// </summary>
        [UnitOfWork]
        public virtual async Task<string> GenerateNewInvoiceNumberAsync()
        {
            var queryable = await _invoiceRepository.GetQueryableAsync().ConfigureAwait(false);

            var maxNumber = queryable
                .Select(i => i.InvoiceNumber)
                .OrderByDescending(c => c)
                .FirstOrDefault();

            // أول رقم في النظام
            if (string.IsNullOrWhiteSpace(maxNumber))
            {
                return "INV-000001";
            }

            // نستخرج الجزء الرقمي من آخر رقم (يدعم وجود Prefix قديم)
            var numericPart = new string(maxNumber.Where(char.IsDigit).ToArray());

            if (!long.TryParse(numericPart, out var maxValue))
            {
                throw new BusinessException("Edary:InvalidInvoiceNumberFormat",
                    $"Cannot generate new InvoiceNumber because existing max number '{maxNumber}' does not contain a valid numeric part.");
            }

            var nextValue = maxValue + 1;

            // نثبّت طول الجزء الرقمي على 6 أرقام
            return $"INV-{nextValue:D6}";
        }
    }
}

