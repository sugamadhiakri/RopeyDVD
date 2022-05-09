using System.ComponentModel.DataAnnotations;

namespace RopeyDVD.Models
{
    public class MembershipCategory
    {
        [Key]
        public int MembershipCategoryId { get; set; }
        public string MembershipCategoryDescription { get; set; } = string.Empty;
        public int MembershipCategoryTotalLoans { get; set; }

        public virtual IEnumerable<Member> Members { get; set; }
    }
}

