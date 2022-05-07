using System.ComponentModel.DataAnnotations;

namespace RopeyDVD.Models
{
    public class DVDCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool AgeRestricted { get; set; }
        public virtual ICollection<DVDTitle> DVDTitles { get; set; }
    }
}
