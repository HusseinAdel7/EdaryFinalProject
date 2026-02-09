using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Edary.DTOs.MainAccounts
{
    public class CreateMainAccountDto
    {
        [Required]
        public string AccountName { get; set; }

        public string? AccountNameEn { get; set; }

        public string? Title { get; set; }
        public string? TitleEn { get; set; }

        public string? TransferredTo { get; set; }
        public string? TransferredToEn { get; set; }

        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        public string? ParentMainAccountId { get; set; }
    }
}
