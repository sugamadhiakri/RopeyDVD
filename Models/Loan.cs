using System.ComponentModel.DataAnnotations.Schema;

namespace RopeyDVD.Models
{
    public class Loan
    {
        public int LoanId { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateOut { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateDue { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateReturned { get; set; }

        [ForeignKey("LoanType")]
        public int LoanTypeID { get; set; }

        [ForeignKey("DVDCopy")]
        public int CopyId { get; set; }

        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public virtual LoanType LoanType { get; set; }

        public virtual DVDCopy Copy { get; set; }
        public virtual Member Member { get; set; }

    }
}
