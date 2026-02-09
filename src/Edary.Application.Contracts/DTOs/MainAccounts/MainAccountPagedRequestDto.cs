using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Edary.DTOs.MainAccounts
{
    public class MainAccountPagedRequestDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
        public bool? IsActive { get; set; }
    }
}
