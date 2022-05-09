using System.ComponentModel.DataAnnotations;

namespace RopeyDVD.Models
{
    public class LoanInputModel
    {
        [Required(ErrorMessage = "Member Number is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Member Number must be a positive number")]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Copy Number is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Copy Number must be a positive number")]
        public int CopyId { get; set; }
        public string loanType { get; set; }
    }
}
