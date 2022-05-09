using System.ComponentModel.DataAnnotations;

namespace RopeyDVD.Models
{
    public class LoanType
    {
        [Key]
        public int LoanTypeId { get; set; }
        public string Type { get; set; } = String.Empty;
        public int Duration { get; set; }

        public IEnumerable<Loan> Loans { get; set; }

    }
}
