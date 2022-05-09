using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RopeyDVD.Models
{
    public class DVDCopy
    {
        [Key]
        public int CopyId { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DatePurchased { get; set; }  

        public bool IsLoaned { get; set; }
        public virtual DVDTitle DVDTitle { get; set; }

        public virtual IEnumerable<Loan> Loans { get; set; }

    }
}
