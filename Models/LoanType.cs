using System.ComponentModel.DataAnnotations;

namespace RopeyDVD.Models
{
    public class LoanType
    {
        [Key]
        public int LoanTypeId { get; set; }
        public string Type { get; set; }
        public int Duration { get; set; }

    }
}
