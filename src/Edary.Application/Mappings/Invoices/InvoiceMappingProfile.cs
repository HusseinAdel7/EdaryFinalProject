using AutoMapper;
using Edary.DTOs.Invoices;
using Edary.Entities.Invoices;

namespace Edary.Mappings.Invoices
{
    public class InvoiceMappingProfile : Profile
    {
        public InvoiceMappingProfile()
        {
            CreateMap<CreateInvoiceDto, Invoice>();
            CreateMap<UpdateInvoiceDto, Invoice>();
            CreateMap<CreateInvoiceDetailDto, InvoiceDetail>();
            CreateMap<UpdateInvoiceDetailDto, InvoiceDetail>();
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<InvoiceDetail, InvoiceDetailDto>();
        }
    }
}

