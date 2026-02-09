using Edary.DTOs.SubAccounts;
using Volo.Abp.Application.Services;

namespace Edary.IAppServices
{
    public interface ISubAccountAppService :
        ICrudAppService<
            SubAccountDto,
            string,
            SubAccountPagedRequestDto,
            CreateSubAccountDto,
            UpdateSubAccountDto
        >
    {
    }
}
