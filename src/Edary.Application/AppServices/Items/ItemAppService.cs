using Edary.Domain.Services.Items;
using Edary.DTOs.Items;
using Edary.Entities.Items;
using Edary.IAppServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Edary.AppServices.Items
{
    public class ItemAppService :
        CrudAppService<
            Item,
            ItemDto,
            string,
            ItemPagedRequestDto,
            CreateItemDto,
            UpdateItemDto>,
        IItemAppService
    {
        private readonly ItemManager _itemManager;
        private readonly IRepository<Item, string> _itemRepository;
        private readonly IRepository<ItemPrice, string> _itemPriceRepository;

        public ItemAppService(
            IRepository<Item, string> itemRepository,
            IRepository<ItemPrice, string> itemPriceRepository,
            ItemManager itemManager)
            : base(itemRepository)
        {
            _itemRepository = itemRepository;
            _itemPriceRepository = itemPriceRepository;
            _itemManager = itemManager;
        }

        public override async Task<ItemDto> GetAsync(string id)
        {
            var query = await _itemRepository
                .WithDetailsAsync(x => x.ItemPrices);

            var item = await query.FirstOrDefaultAsync(x => x.Id == id);

            return ObjectMapper.Map<Item, ItemDto>(item);
        }

        public override async Task<PagedResultDto<ItemDto>> GetListAsync(ItemPagedRequestDto input)
        {
            var query = await _itemRepository.GetQueryableAsync();

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                query = query.Where(i =>
                    i.ItemCode.Contains(input.Filter) ||
                    i.ItemName.Contains(input.Filter) ||
                    (i.ItemNameEn != null && i.ItemNameEn.Contains(input.Filter)) ||
                    (i.Barcode != null && i.Barcode.Contains(input.Filter)));
            }

            if (input.IsActive.HasValue)
            {
                query = query.Where(i => i.IsActive == input.IsActive.Value);
            }

            if (!string.IsNullOrWhiteSpace(input.GroupName))
            {
                query = query.Where(i => i.GroupName == input.GroupName);
            }

            if (!string.IsNullOrWhiteSpace(input.ItemType))
            {
                query = query.Where(i => i.ItemType == input.ItemType);
            }

            query = !string.IsNullOrWhiteSpace(input.Sorting)
                ? query.OrderBy(input.Sorting)
                : query.OrderByDescending(i => i.CreationTime);

            var totalCount = await AsyncExecuter.CountAsync(query);
            query = query.PageBy(input.SkipCount, input.MaxResultCount);

            var entities = await AsyncExecuter.ToListAsync(query);
            var dtos = ObjectMapper.Map<List<Item>, List<ItemDto>>(entities);

            return new PagedResultDto<ItemDto>(totalCount, dtos);
        }

        public override async Task<ItemDto> CreateAsync(CreateItemDto input)
        {
            // توليد كود الصنف دائماً من السيرفر (لا يؤخذ من الواجهة)
            var generatedCode = await _itemManager.GenerateNewItemCodeAsync();

            var item = ObjectMapper.Map<CreateItemDto, Item>(input);
            item.ItemCode = generatedCode;

            // نضمن وجود Id للصنف
            Volo.Abp.Domain.Entities.EntityHelper.TrySetId(
                item,
                () => GuidGenerator.Create().ToString());

            // نضمن Id و ItemId لكل سعر مرتبط
            foreach (var price in item.ItemPrices)
            {
                Volo.Abp.Domain.Entities.EntityHelper.TrySetId(
                    price,
                    () => GuidGenerator.Create().ToString());

                price.ItemId = item.Id;
            }

            var created = await _itemRepository.InsertAsync(item, autoSave: true);
            return ObjectMapper.Map<Item, ItemDto>(created);
        }

        public override async Task<ItemDto> UpdateAsync(string id, UpdateItemDto input)
        {
            // نجلب الصنف مع تفاصيل الأسعار في نفس DbContext (Tracked)
            var query = await _itemRepository.WithDetailsAsync(x => x.ItemPrices);
            var item = await query.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                throw new EntityNotFoundException(typeof(Item), id);
            }

            // لا نسمح بتعديل ItemCode
            ObjectMapper.Map(input, item);

            // الأسعار الموجودة حالياً
            var existingPricesById = item.ItemPrices
                .Where(p => !string.IsNullOrEmpty(p.Id))
                .ToDictionary(p => p.Id, p => p);

            foreach (var priceDto in input.ItemPrices)
            {
                if (string.IsNullOrEmpty(priceDto.Id))
                {
                    // سعر جديد
                    var newPrice = ObjectMapper.Map<UpdateItemPriceDto, ItemPrice>(priceDto);

                    newPrice.ItemId = id;

                    Volo.Abp.Domain.Entities.EntityHelper.TrySetId(
                        newPrice,
                        () => GuidGenerator.Create().ToString());

                    item.ItemPrices.Add(newPrice);
                }
                else if (existingPricesById.TryGetValue(priceDto.Id, out var existingPrice))
                {
                    // تحديث السعر الموجود (نفس الـ instance المتتبع)
                    ObjectMapper.Map(priceDto, existingPrice);
                }
            }

            // حذف الأسعار التي لم تعد موجودة
            var inputPriceIds = input.ItemPrices
                .Where(p => !string.IsNullOrEmpty(p.Id))
                .Select(p => p.Id)
                .ToHashSet();

            var pricesToRemove = item.ItemPrices
                .Where(p => !string.IsNullOrEmpty(p.Id) && !inputPriceIds.Contains(p.Id))
                .ToList();

            foreach (var price in pricesToRemove)
            {
                item.ItemPrices.Remove(price);
            }

            // 🔥 هنا الفرق المهم
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<Item, ItemDto>(item);
        }

        public override async Task DeleteAsync(string id)
        {
            // حذف أسعار الصنف أولاً ثم حذف الصنف
            var priceIdsToDelete = await (await _itemPriceRepository.GetQueryableAsync())
                .Where(p => p.ItemId == id)
                .Select(p => p.Id)
                .ToListAsync();

            if (priceIdsToDelete.Any())
            {
                await _itemPriceRepository.DeleteManyAsync(priceIdsToDelete);
            }

            await _itemRepository.DeleteAsync(id);
        }
    }
}

