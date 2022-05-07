namespace RopeyDVD.Models
{
    public class Producer
    {
        public int ProducerId { get; set; }
        public string ProducerName { get; set; } 

        public virtual ICollection<DVDTitle> DVDTitles { get; set; }
    }
}
