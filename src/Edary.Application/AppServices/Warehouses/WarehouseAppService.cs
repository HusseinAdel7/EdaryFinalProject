using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Edary.Domain.Services.Warehouses;
using Edary.DTOs.Warehouses;
using Edary.Entities.Warehouses;
using Edary.IAppServices;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Edary.AppServices.Warehouses
{
    public class WarehouseAppService :
        CrudAppService<
            Warehouse,
            WarehouseDto,
            string,
            WarehousePagedRequestDto,
            CreateWarehouseDto,
            UpdateWarehouseDto>,
        IWarehouseAppService
    {
        private readonly WarehouseManager _warehouseManager;

        public WarehouseAppService(
            IRepository<Warehouse, string> repository,
            WarehouseManager warehouseManager)
            : base(repository)
        {
            _warehouseManager = warehouseManager;
        }

        public override async Task<WarehouseDto> CreateAsync(CreateWarehouseDto input)
        {
            // توليد كود المخزن دائماً من السيرفر (لا يؤخذ من الواجهة)
            var generatedCode = await _warehouseManager.GenerateNewWarehouseCodeAsync();

            var warehouse = ObjectMapper.Map<CreateWarehouseDto, Warehouse>(input);
            warehouse.WarehouseCode = generatedCode;

            // نضمن وجود Id
            Volo.Abp.Domain.Entities.EntityHelper.TrySetId(
                warehouse,
                () => GuidGenerator.Create().ToString());

            var created = await Repository.InsertAsync(warehouse, autoSave: true);
            return MapToGetOutputDto(created);
        }

        public override async Task<WarehouseDto> UpdateAsync(string id, UpdateWarehouseDto input)
        {
            var warehouse = await Repository.GetAsync(id);

            // لا نسمح بتعديل WarehouseCode من الـ Update
            warehouse.WarehouseName = input.WarehouseName;
            warehouse.Location = input.Location;
            warehouse.ManagerName = input.ManagerName;
            warehouse.Notes = input.Notes;
            warehouse.IsActive = input.IsActive;
            warehouse.WarehouseNameEn = input.WarehouseNameEn;
            warehouse.ManagerNameEn = input.ManagerNameEn;

            var updated = await Repository.UpdateAsync(warehouse, autoSave: true);
            return MapToGetOutputDto(updated);
        }

        public override async Task<PagedResultDto<WarehouseDto>> GetListAsync(WarehousePagedRequestDto input)
        {
            var query = await Repository.GetQueryableAsync();

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                query = query.Where(w =>
                    w.WarehouseCode.Contains(input.Filter) ||
                    w.WarehouseName.Contains(input.Filter) ||
                    (w.WarehouseNameEn != null && w.WarehouseNameEn.Contains(input.Filter)) ||
                    (w.Location != null && w.Location.Contains(input.Filter)) ||
                    (w.ManagerName != null && w.ManagerName.Contains(input.Filter)) ||
                    (w.ManagerNameEn != null && w.ManagerNameEn.Contains(input.Filter))
                );
            }

            if (input.IsActive.HasValue)
            {
                query = query.Where(w => w.IsActive == input.IsActive.Value);
            }

            query = !string.IsNullOrWhiteSpace(input.Sorting)
                ? query.OrderBy(input.Sorting)
                : query.OrderByDescending(w => w.CreationTime);

            var totalCount = await AsyncExecuter.CountAsync(query);
            query = query.PageBy(input.SkipCount, input.MaxResultCount);

            var entities = await AsyncExecuter.ToListAsync(query);
            var dtos = entities.Select(MapToGetOutputDto).ToList();

            return new PagedResultDto<WarehouseDto>(totalCount, dtos);
        }
    }
}

