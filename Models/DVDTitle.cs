using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RopeyDVD.Models
{
    public class DVDTitle
    {
        [Key]
        public int DVDId { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateReleased { get; set; }
        public double StandardCharge { get; set; }
        public double PenaltyCharge { get; set; }

        [ForeignKey("DVDCategory")]
        public int CategoryId { get; set; }

        [ForeignKey("Studio")]
        public int StudioId { get; set; }

        [ForeignKey("Producer")]
        public int ProducerId { get; set; }

        public virtual Producer Producer { get; set; }
        public virtual DVDCategory Category { get; set; }
        public virtual Studio Studio { get; set; }

        public virtual ICollection<DVDCopy> DVDCopies { get; set; }
        public virtual ICollection<CastMember> CastMembers { get; set; }
        public virtual ICollection<Actor> Actors { get; set; }
    }
}
