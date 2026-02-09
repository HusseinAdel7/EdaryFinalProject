using AutoMapper;
using Edary.DTOs.SubAccounts;
using Edary.Entities.SubAccounts;

namespace Edary.Application.Mappings.SubAccounts
{
    public class SubAccountMappingProfile : Profile
    {
        public SubAccountMappingProfile()
        {
            CreateMap<SubAccount, SubAccountDto>();
            CreateMap<CreateSubAccountDto, SubAccount>();
            CreateMap<UpdateSubAccountDto, SubAccount>();
        }
    }
}

