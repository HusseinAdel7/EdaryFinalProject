using Volo.Abp.Application.Services;
using Edary.DTOs.Invoices;

namespace Edary.IAppServices
{
    public interface IInvoiceAppService :
        ICrudAppService<
            InvoiceDto,
            string,
            InvoicePagedRequestDto,
            CreateInvoiceDto,
            UpdateInvoiceDto>
    {
    }
}

