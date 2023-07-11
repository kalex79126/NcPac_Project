using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NcPac_Project2.Models
{
    public class Division
    {
        public int DivisionID { get; set; }

        [Required(ErrorMessage = "You cannot leave the Division Title blank.")]
        [StringLength(100, ErrorMessage = "Division Title cannot be more than 100 characters long.")]
        public string DivisionTitle { get; set; }

        [Required(ErrorMessage = "You cannot leave the Division Field blank.")]
        [StringLength(20, ErrorMessage = "Division Field cannot be more than 20 characters long.")]
        public ICollection<Committee> Committees { get; set; } = new HashSet<Committee>();
    }
}
