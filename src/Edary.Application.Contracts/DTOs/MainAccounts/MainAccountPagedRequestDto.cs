using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Edary.DTOs.MainAccounts
{
    public class MainAccountPagedRequestDto : PagedAndSortedResultRequestDto
    {
        [StringLength(500, ErrorMessage = "نص البحث لا يمكن أن يتجاوز 500 حرف")]
        public string? Filter { get; set; }

        public bool? IsActive { get; set; }
    }
}
