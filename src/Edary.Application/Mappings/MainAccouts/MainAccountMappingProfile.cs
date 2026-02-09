using AutoMapper;
using Edary.DTOs;
using Edary.DTOs.MainAccounts;
using Edary.Entities.MainAccounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edary.Mappings.MainAccouts
{
    public class MainAccountMappingProfile : Profile
    {
        public MainAccountMappingProfile()
        {
            CreateMap<MainAccount, MainAccountDto>();
            CreateMap<CreateMainAccountDto, MainAccount>();
            CreateMap<UpdateMainAccountDto, MainAccount>();

        }
    }
}
