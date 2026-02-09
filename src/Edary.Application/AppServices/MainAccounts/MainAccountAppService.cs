using Edary.DTOs.MainAccounts;
using Edary.Entities.MainAccounts;
using Edary.IAppServices;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Edary.Domain.Services.MainAccounts;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp.Domain.Entities;
using System.Linq.Dynamic.Core;
using Volo.Abp.Application.Dtos;

namespace Edary.AppServices.MainAccounts
{
    public class MainAccountAppService
    : CrudAppService<
        MainAccount,
        MainAccountDto,
        string,
        MainAccountPagedRequestDto,
        CreateMainAccountDto,
        UpdateMainAccountDto>,
      IMainAccountAppService
    {
        private readonly MainAccountManager _mainAccountManager;

        public MainAccountAppService(
            IRepository<MainAccount, string> repository,
            MainAccountManager mainAccountManager)
            : base(repository)
        {
            _mainAccountManager = mainAccountManager;
        }

        public override async Task<MainAccountDto> CreateAsync(CreateMainAccountDto input)
        {
            var newAccountId = GuidGenerator.Create().ToString();
            var newAccountNumber = await _mainAccountManager.GenerateNewAccountNumberAsync(input.ParentMainAccountId);

            var mainAccount = new MainAccount(newAccountId, newAccountNumber)
            {
                AccountName = input.AccountName,
                AccountNameEn = input.AccountNameEn,
                Title = input.Title,
                TitleEn = input.TitleEn,
                TransferredTo = input.TransferredTo,
                TransferredToEn = input.TransferredToEn,
                IsActive = input.IsActive,
                Notes = input.Notes,
                ParentMainAccountId = input.ParentMainAccountId
            };

            var createdAccount = await Repository.InsertAsync(mainAccount, autoSave: true);

            return MapToGetOutputDto(createdAccount);
        }

        public override async Task<MainAccountDto> UpdateAsync(string id, UpdateMainAccountDto input)
        {
            var mainAccount = await Repository.GetAsync(id);



            mainAccount.AccountName = input.AccountName;
            mainAccount.AccountNameEn = input.AccountNameEn;
            mainAccount.Title = input.Title;
            mainAccount.TitleEn = input.TitleEn;
            mainAccount.TransferredTo = input.TransferredTo;
            mainAccount.TransferredToEn = input.TransferredToEn;
            mainAccount.IsActive = input.IsActive;
            mainAccount.Notes = input.Notes;
            mainAccount.ParentMainAccountId = input.ParentMainAccountId;

            var updatedAccount = await Repository.UpdateAsync(mainAccount, autoSave: true);

            return MapToGetOutputDto(updatedAccount);
        }

        public override async Task<PagedResultDto<MainAccountDto>> GetListAsync(MainAccountPagedRequestDto input)
        {
            var query = await Repository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
            {
                query = query.Where(ma =>
                    ma.AccountName.Contains(input.Filter) ||
                    (ma.AccountNameEn != null && ma.AccountNameEn.Contains(input.Filter)) ||
                    (ma.AccountNumber != null && ma.AccountNumber.Contains(input.Filter)) ||
                    (ma.Title != null && ma.Title.Contains(input.Filter)) ||
                    (ma.TitleEn != null && ma.TitleEn.Contains(input.Filter))
                );
            }

            if (input.IsActive.HasValue)
            {
                query = query.Where(ma => ma.IsActive == input.IsActive.Value);
            }

            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                query = query.OrderBy(input.Sorting);
            }
            else
            {
                query = query.OrderByDescending(ma => ma.CreationTime);
            }

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = query.PageBy(input.SkipCount, input.MaxResultCount);

            var entities = await AsyncExecuter.ToListAsync(query);
            var dtos = entities.Select(MapToGetOutputDto).ToList();

            return new PagedResultDto<MainAccountDto>(totalCount, dtos);
        }
    }
}
