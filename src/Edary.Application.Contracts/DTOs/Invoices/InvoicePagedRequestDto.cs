using Volo.Abp.Application.Dtos;

namespace Edary.DTOs.Invoices
{
    public class InvoicePagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
        public string? InvoiceType { get; set; }
        public string? PaymentStatus { get; set; }
    }
}

