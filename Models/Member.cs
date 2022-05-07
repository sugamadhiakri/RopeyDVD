using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RopeyDVD.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        public string FirstName { get; set; }  
        public string LastName { get; set; } 
        public string Address { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Dob { get; set; }

        [ForeignKey("MembershipCategory")]
        public int MemberCategoryId { get; set; }
        public virtual MembershipCategory MembershipCategory { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }

        [NotMapped]
        public virtual int LoanCount { get; set; }
    }
}
