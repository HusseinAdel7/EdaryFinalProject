using Edary.DTOs.Items;
using Volo.Abp.Application.Services;

namespace Edary.IAppServices
{
    public interface IItemAppService :
        ICrudAppService<
            ItemDto,
            string,
            ItemPagedRequestDto,
            CreateItemDto,
            UpdateItemDto>
    {
    }
}

