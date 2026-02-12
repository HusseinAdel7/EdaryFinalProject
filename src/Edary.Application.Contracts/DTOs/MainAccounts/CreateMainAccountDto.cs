using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Edary.Consts.MainAccounts;

namespace Edary.DTOs.MainAccounts
{
    public class CreateMainAccountDto
    {
        [Required(ErrorMessage = "اسم الحساب مطلوب")]
        [StringLength(MainAccountConsts.MaxAccountNameLength, ErrorMessage = "اسم الحساب لا يمكن أن يتجاوز {1} حرف")]
        public string AccountName { get; set; } = string.Empty;

        [StringLength(MainAccountConsts.MaxAccountNameEnLength, ErrorMessage = "اسم الحساب بالإنجليزية لا يمكن أن يتجاوز {1} حرف")]
        public string? AccountNameEn { get; set; }

        [StringLength(MainAccountConsts.MaxTitleLength, ErrorMessage = "العنوان لا يمكن أن يتجاوز {1} حرف")]
        public string? Title { get; set; }

        [StringLength(MainAccountConsts.MaxTitleEnLength, ErrorMessage = "العنوان بالإنجليزية لا يمكن أن يتجاوز {1} حرف")]
        public string? TitleEn { get; set; }

        [StringLength(MainAccountConsts.MaxTransferredToLength, ErrorMessage = "المنقول إليه لا يمكن أن يتجاوز {1} حرف")]
        public string? TransferredTo { get; set; }

        [StringLength(MainAccountConsts.MaxTransferredToEnLength, ErrorMessage = "المنقول إليه بالإنجليزية لا يمكن أن يتجاوز {1} حرف")]
        public string? TransferredToEn { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(MainAccountConsts.MaxNotesLength, ErrorMessage = "الملاحظات لا يمكن أن تتجاوز {1} حرف")]
        public string? Notes { get; set; }

        public string? ParentMainAccountId { get; set; }
    }
}
