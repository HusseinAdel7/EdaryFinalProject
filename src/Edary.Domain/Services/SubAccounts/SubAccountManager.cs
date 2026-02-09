using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using Edary.Entities.MainAccounts;
using Edary.Entities.SubAccounts;
using Volo.Abp.Uow;

namespace Edary.Domain.Services.SubAccounts
{
    public class SubAccountManager : DomainService
    {
        private readonly IRepository<SubAccount, string> _subAccountRepository;
        private readonly IRepository<MainAccount, string> _mainAccountRepository;

        public SubAccountManager(
            IRepository<SubAccount, string> subAccountRepository,
            IRepository<MainAccount, string> mainAccountRepository)
        {
            _subAccountRepository = subAccountRepository;
            _mainAccountRepository = mainAccountRepository;
        }

        [UnitOfWork]
        public virtual async Task<string> GenerateNewAccountNumberAsync(string mainAccountId)
        {
            var mainAccount = await _mainAccountRepository.GetAsync(mainAccountId);

            if (mainAccount == null || !mainAccount.IsActive)
            {
                throw new BusinessException("Edary:MainAccountNotFoundOrInactive",
                    $"Main account with ID {mainAccountId} not found or is inactive.");
            }

            var queryable = await _subAccountRepository.GetQueryableAsync();
            var maxSubAccountNumber = queryable
                .Where(sa => sa.MainAccountId == mainAccountId)
                .Select(sa => sa.AccountNumber)
                .OrderByDescending(an => an)
                .FirstOrDefault();

            long newAccountNumberValue;

            if (string.IsNullOrEmpty(maxSubAccountNumber))
            {
                newAccountNumberValue = (long.Parse(mainAccount.AccountNumber) * 10) + 1;
            }
            else
            {
                newAccountNumberValue = long.Parse(maxSubAccountNumber) + 1;
            }

            return newAccountNumberValue.ToString();
        }
    }
}
