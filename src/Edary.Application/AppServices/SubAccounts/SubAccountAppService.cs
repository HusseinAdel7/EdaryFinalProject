using Edary.DTOs.SubAccounts;
using Edary.Entities.SubAccounts;
using Edary.IAppServices;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Edary.Domain.Services.SubAccounts;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using Volo.Abp.Application.Dtos;

namespace Edary.AppServices.SubAccounts
{
    public class SubAccountAppService
    : CrudAppService<
        SubAccount,
        SubAccountDto,
        string,
        SubAccountPagedRequestDto,
        CreateSubAccountDto,
        UpdateSubAccountDto>,
      ISubAccountAppService
    {
        private readonly SubAccountManager _subAccountManager;

        public SubAccountAppService(
            IRepository<SubAccount, string> repository,
            SubAccountManager subAccountManager)
            : base(repository)
        {
            _subAccountManager = subAccountManager;
        }

        public override async Task<SubAccountDto> CreateAsync(CreateSubAccountDto input)
        {
            var newAccountId = GuidGenerator.Create().ToString();
            var newAccountNumber = await _subAccountManager.GenerateNewAccountNumberAsync(input.MainAccountId);

            var subAccount = new SubAccount(newAccountId, newAccountNumber)
            {
                AccountName = input.AccountName,
                MainAccountId = input.MainAccountId,
                Title = input.Title,
                AccountType = input.AccountType,
                CreditAmount = input.CreditAmount,
                StandardCreditRate = input.StandardCreditRate,
                Commission = input.Commission,
                Percentage = input.Percentage,
                AccountCurrency = input.AccountCurrency,
                Notes = input.Notes,
                IsActive = input.IsActive,
                AccountNameEn = input.AccountNameEn,
                TitleEn = input.TitleEn,
                AccountTypeEn = input.AccountTypeEn,
                AccountCurrencyEn = input.AccountCurrencyEn
            };

            var createdAccount = await Repository.InsertAsync(subAccount, autoSave: true);

            return MapToGetOutputDto(createdAccount);
        }

        public override async Task<SubAccountDto> UpdateAsync(string id, UpdateSubAccountDto input)
        {
            var subAccount = await Repository.GetAsync(id);



            subAccount.AccountName = input.AccountName;
            subAccount.MainAccountId = input.MainAccountId;
            subAccount.Title = input.Title;
            subAccount.AccountType = input.AccountType;
            subAccount.CreditAmount = input.CreditAmount;
            subAccount.StandardCreditRate = input.StandardCreditRate;
            subAccount.Commission = input.Commission;
            subAccount.Percentage = input.Percentage;
            subAccount.AccountCurrency = input.AccountCurrency;
            subAccount.Notes = input.Notes;
            subAccount.IsActive = input.IsActive;
            subAccount.AccountNameEn = input.AccountNameEn;
            subAccount.TitleEn = input.TitleEn;
            subAccount.AccountTypeEn = input.AccountTypeEn;
            subAccount.AccountCurrencyEn = input.AccountCurrencyEn;

            var updatedAccount = await Repository.UpdateAsync(subAccount, autoSave: true);

            return MapToGetOutputDto(updatedAccount);
        }

        public override async Task<PagedResultDto<SubAccountDto>> GetListAsync(SubAccountPagedRequestDto input)
        {
            var query = await Repository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
            {
                query = query.Where(sa =>
                    sa.AccountName.Contains(input.Filter) ||
                    (sa.AccountNameEn != null && sa.AccountNameEn.Contains(input.Filter)) ||
                    (sa.AccountNumber != null && sa.AccountNumber.Contains(input.Filter)) ||
                    (sa.Title != null && sa.Title.Contains(input.Filter)) ||
                    (sa.TitleEn != null && sa.TitleEn.Contains(input.Filter))
                );
            }

            if (input.IsActive.HasValue)
            {
                query = query.Where(sa => sa.IsActive == input.IsActive.Value);
            }

            if (!string.IsNullOrEmpty(input.MainAccountId))
            {
                query = query.Where(sa => sa.MainAccountId == input.MainAccountId);
            }

            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                query = query.OrderBy(input.Sorting);
            }
            else
            {
                query = query.OrderByDescending(sa => sa.CreationTime); 
            }

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = query.PageBy(input.SkipCount, input.MaxResultCount);

            var entities = await AsyncExecuter.ToListAsync(query);
            var dtos = entities.Select(MapToGetOutputDto).ToList();

            return new PagedResultDto<SubAccountDto>(totalCount, dtos);
        }
    }
}
