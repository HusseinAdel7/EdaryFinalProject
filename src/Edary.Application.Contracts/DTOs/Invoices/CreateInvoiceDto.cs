using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Edary.Consts.Invoices;

namespace Edary.DTOs.Invoices
{
    public class CreateInvoiceDto
    {
        [Required]
        [StringLength(InvoiceConsts.MaxInvoiceTypeLength)]
        public string InvoiceType { get; set; }

        public string? SupplierId { get; set; }

        [Required]
        public string WarehouseId { get; set; }

        [StringLength(InvoiceConsts.MaxCurrencyLength)]
        public string Currency { get; set; }

        public decimal? TotalAmount { get; set; }

        public decimal? Discount { get; set; }

        public decimal? TaxAmount { get; set; }

        [StringLength(InvoiceConsts.MaxPaymentStatusLength)]
        public string PaymentStatus { get; set; }

        [StringLength(InvoiceConsts.MaxNotesLength)]
        public string Notes { get; set; }

        [MinLength(1, ErrorMessage = "An invoice must have at least one detail line.")]
        public ICollection<CreateInvoiceDetailDto> InvoiceDetails { get; set; } = new HashSet<CreateInvoiceDetailDto>();
    }
}

