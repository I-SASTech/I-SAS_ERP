using System.ComponentModel.DataAnnotations;

namespace Focus.Domain.Enum
{
  public  enum ApprovalStatus
    {
        [Display(Name = "Pending")]
        Pending  = 1,
        [Display(Name = "Rejected")]
        Rejected  = 2,
        [Display(Name = "Approved")]
        Approved = 3,
        [Display(Name = "Draft")]
        Draft  = 4,
        [Display(Name = "InProcess")]
        InProcess = 5,
        [Display(Name = "Complete")]
        Complete = 6,
        [Display(Name = "Transfer")]
        Transfer = 7,
        [Display(Name = "Void")]
        Void = 8,
        [Display(Name = "Confirm")]
        Confirm = 9,
    }
}
