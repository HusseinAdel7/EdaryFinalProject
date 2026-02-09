using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Edary.DTOs.MainAccounts
{
    // DTO for updating existing records
    public class UpdateMainAccountDto
    {
        [Required]
        public string AccountName { get; set; }

        public string? AccountNameEn { get; set; }

        public string? Title { get; set; }
        public string? TitleEn { get; set; }

        public string? TransferredTo { get; set; }
        public string? TransferredToEn { get; set; }

        public bool IsActive { get; set; }
        public string? Notes { get; set; }

        public string? ParentMainAccountId { get; set; }
    }
}
