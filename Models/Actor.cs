using System.ComponentModel.DataAnnotations;

namespace RopeyDVD.Models
{
    public class Actor
    {
        [Key]
        public int ActorId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;

        public virtual IEnumerable<DVDTitle> DVDTitles { get; set; }
    }
}
