namespace RopeyDVD.Models
{
    public class DVDCopy
    {
        public int CopyId { get; set; }
        public DVDTitle? DVD { get; set; }

        public DateTime? DatePurchased { get; set; }
    }
}
