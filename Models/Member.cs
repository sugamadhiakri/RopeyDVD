namespace RopeyDVD.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; } = string.Empty;   
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public MembershipCategory? MembershipCategory { get; set; }
    }
}
