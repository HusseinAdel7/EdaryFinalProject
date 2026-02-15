using Edary.DTOs.TrailBalances;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Edary.IAppServices
{
    public interface ITrialBalanceAppService : IApplicationService
    {
        public interface ITrialBalanceAppService : IApplicationService
        {
            Task<List<TrialBalanceLineDto>> GetAsync(
                DateTime? fromDate,
                DateTime? toDate);

            Task<List<TrialBalanceLineDto>> GetForAccountAsync(
                string accountId,
                DateTime? fromDate,
                DateTime? toDate);
        }
    }
}
