using System.ComponentModel.DataAnnotations;

namespace RopeyDVD.Models
{
    public class SearchInt
    {
        [Required(ErrorMessage = "This Field is Required")]
        public int SearchValue { get; set; }
    }
}
