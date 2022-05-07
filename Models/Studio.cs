namespace RopeyDVD.Models
{
    public class Studio
    {
        public int StudioId { get; set; }  
        public string StudioName { get; set; } 

        public virtual ICollection<DVDTitle> DVDTitles { get; set; }

    }
}
