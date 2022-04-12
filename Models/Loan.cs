namespace RopeyDVD.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime DateReturned { get; set; }

        public LoanType? LoanType { get; set; }

        public DVDCopy? Copy { get; set; }
        public Member? Member { get; set; }

    }
}
