using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RopeyDVD.Models
{
    [Keyless]
    public class CastMember
    {
        [ForeignKey("DVDTitle")]
        public int DVDId { get; set; }
        [ForeignKey("Actor")]
        public int ActorId { get; set; }
    }
}
