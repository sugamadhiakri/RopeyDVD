namespace RopeyDVD.Models
{
    public class DVDTitle
    {
        public int DVDId { get; set; }
        public DateTime DateReleased { get; set; }
        public double StandardCharge { get; set; }
        public double PenaltyCharge { get; set; }

        public Producer? Producer { get; set; }
        public DVDCategory? Category { get; set; }
        public Studio? Studio { get; set; }

    }
}
