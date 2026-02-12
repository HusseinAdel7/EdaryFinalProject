using System.ComponentModel.DataAnnotations;
using Edary.Consts.Invoices;

namespace Edary.DTOs.Invoices
{
    public class CreateInvoiceDetailDto
    {
        [Required]
        public string ItemId { get; set; }

        [Required]
        [StringLength(InvoiceDetailConsts.MaxUnitNameLength)]
        public string UnitName { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Quantity { get; set; }

        [Range(0.0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        public decimal? Discount { get; set; }

        public decimal? TaxRate { get; set; }
    }
}

