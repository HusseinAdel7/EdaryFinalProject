using Edary.DTOs.TrailBalances;
using Edary.IAppServices;
using Edary.Services.TrailBalances;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Edary.AppServices.TrailBalances
{
    public class TrialBalanceAppService : ApplicationService, ITrialBalanceAppService
    {
        private readonly TrialBalanceManager _manager;

        public TrialBalanceAppService(TrialBalanceManager manager)
        {
            _manager = manager;
        }

        public async Task<List<TrialBalanceLineDto>> GetAsync(
     DateTime? fromDate,
     DateTime? toDate)
        {
            return await _manager.GenerateAsync(fromDate, toDate);
        }

        public async Task<List<TrialBalanceLineDto>> GetForAccountAsync(
            string accountId,
            DateTime? fromDate,
            DateTime? toDate)
        {
            return await _manager.GenerateAsync(fromDate, toDate, accountId);
        }

    }

}
