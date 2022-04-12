namespace RopeyDVD.Models
{
    public class DVDCategory
    {
        public int CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool AgeRestricted { get; set; }
    }
}
