using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Edary.Domain.Services.Invoices;
using Edary.DTOs.Invoices;
using Edary.Entities.Invoices;
using Edary.Entities.JournalEntries;
using Edary.IAppServices;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Edary.AppServices.Invoices
{
    public class InvoiceAppService :
        CrudAppService<
            Invoice,
            InvoiceDto,
            string,
            InvoicePagedRequestDto,
            CreateInvoiceDto,
            UpdateInvoiceDto>,
        IInvoiceAppService
    {
        private readonly InvoiceManager _invoiceManager;
        private readonly IRepository<InvoiceDetail, string> _invoiceDetailRepository;
        private readonly IRepository<JournalEntry, string> _journalEntryRepository;

        public InvoiceAppService(
            IRepository<Invoice, string> invoiceRepository,
            IRepository<InvoiceDetail, string> invoiceDetailRepository,
            IRepository<JournalEntry, string> journalEntryRepository,
            InvoiceManager invoiceManager)
            : base(invoiceRepository)
        {
            _invoiceManager = invoiceManager;
            _invoiceDetailRepository = invoiceDetailRepository;
            _journalEntryRepository = journalEntryRepository;
        }

        public override async Task<InvoiceDto> GetAsync(string id)
        {
            var query = await Repository.WithDetailsAsync(x => x.InvoiceDetails);
            var invoice = query.FirstOrDefault(x => x.Id == id);
            return ObjectMapper.Map<Invoice, InvoiceDto>(invoice);
        }

        public override async Task<PagedResultDto<InvoiceDto>> GetListAsync(InvoicePagedRequestDto input)
        {
            var query = await Repository.WithDetailsAsync(x => x.InvoiceDetails);

            query = query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                    invoice => invoice.InvoiceNumber.Contains(input.Filter) ||
                               invoice.Notes.Contains(input.Filter) ||
                               invoice.Currency.Contains(input.Filter))
                .WhereIf(!input.InvoiceType.IsNullOrWhiteSpace(),
                    invoice => invoice.InvoiceType.Contains(input.InvoiceType))
                .WhereIf(!input.PaymentStatus.IsNullOrWhiteSpace(),
                    invoice => invoice.PaymentStatus.Contains(input.PaymentStatus));

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = query.OrderBy(input.Sorting.IsNullOrWhiteSpace()
                    ? "InvoiceNumber asc"
                    : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var invoices = await AsyncExecuter.ToListAsync(query);

            return new PagedResultDto<InvoiceDto>(
                totalCount,
                ObjectMapper.Map<List<Invoice>, List<InvoiceDto>>(invoices));
        }

        public override async Task<InvoiceDto> CreateAsync(CreateInvoiceDto input)
        {
            // 1- توليد رقم فاتورة فريد
            var invoiceNumber = await _invoiceManager.GenerateNewInvoiceNumberAsync();

            // 2- إنشاء قيد اليومية (JournalEntry) وحفظه
            var journalEntryId = GuidGenerator.Create().ToString();
            var journalEntry = new JournalEntry(journalEntryId)
            {
                Currency = input.Currency,
                ExchangeRate = 1,
                Notes = $"Auto generated from invoice {invoiceNumber}",
                CurrencyEn = input.Currency
            };

            await _journalEntryRepository.InsertAsync(journalEntry, autoSave: true);

            // 3- إنشاء الفاتورة وربطها بالقيد
            var invoice = ObjectMapper.Map<CreateInvoiceDto, Invoice>(input);

            Volo.Abp.Domain.Entities.EntityHelper.TrySetId(
                invoice,
                () => GuidGenerator.Create().ToString());

            invoice.InvoiceNumber = invoiceNumber;
            invoice.JournalEntryId = journalEntry.Id;

            // 4- تجهيز تفاصيل الفاتورة وربطها بالـ InvoiceId
            foreach (var detail in invoice.InvoiceDetails)
            {
                detail.GetType()
                      .GetProperty("Id")
                      ?.SetValue(detail, GuidGenerator.Create().ToString());

                detail.InvoiceId = invoice.Id;
            }

            var created = await Repository.InsertAsync(invoice, autoSave: true);

            return ObjectMapper.Map<Invoice, InvoiceDto>(created);
        }

        public override async Task<InvoiceDto> UpdateAsync(string id, UpdateInvoiceDto input)
        {
            var invoice = await Repository.GetAsync(id);
            ObjectMapper.Map(input, invoice);

            // تحديث / إضافة تفاصيل الفاتورة
            foreach (var detailDto in input.InvoiceDetails)
            {
                if (string.IsNullOrEmpty(detailDto.Id))
                {
                    // سطر جديد
                    var newDetail = ObjectMapper.Map<UpdateInvoiceDetailDto, InvoiceDetail>(detailDto);
                    newDetail.InvoiceId = id;
                    await _invoiceDetailRepository.InsertAsync(newDetail);
                }
                else
                {
                    // سطر موجود
                    var existingDetail = await _invoiceDetailRepository.GetAsync(detailDto.Id);
                    ObjectMapper.Map(detailDto, existingDetail);
                    await _invoiceDetailRepository.UpdateAsync(existingDetail);
                }
            }

            // حذف التفاصيل التي لم تعد موجودة في الـ input
            var detailIdsToRemove = invoice.InvoiceDetails.Select(x => x.Id)
                .Except(input.InvoiceDetails.Where(x => !string.IsNullOrEmpty(x.Id)).Select(x => x.Id))
                .ToList();

            foreach (var detailId in detailIdsToRemove)
            {
                await _invoiceDetailRepository.DeleteAsync(detailId);
            }

            var updated = await Repository.UpdateAsync(invoice, autoSave: true);
            return ObjectMapper.Map<Invoice, InvoiceDto>(updated);
        }

        public override async Task DeleteAsync(string id)
        {
            // حذف تفاصيل الفاتورة أولاً
            var detailIdsToDelete = await (await _invoiceDetailRepository.GetQueryableAsync())
                .Where(x => x.InvoiceId == id)
                .Select(x => x.Id)
                .ToListAsync();

            await _invoiceDetailRepository.DeleteManyAsync(detailIdsToDelete);

            await Repository.DeleteAsync(id);
        }
    }
}

