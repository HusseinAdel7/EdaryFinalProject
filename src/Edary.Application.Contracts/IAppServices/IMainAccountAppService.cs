using Edary.DTOs.MainAccounts;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Edary.IAppServices
{
    public interface IMainAccountAppService :
       ICrudAppService<
           MainAccountDto,              
           string,                     
           MainAccountPagedRequestDto,  
           CreateMainAccountDto,        
           UpdateMainAccountDto         
       >
    {
       

        
    }
}
