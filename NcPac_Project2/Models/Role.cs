using Org.BouncyCastle.Bcpg.OpenPgp;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NcPac_Project2.Models
{
    public class Role
    {
        public int RoleID { get; set; }

        [Display(Name = "Role Name")]
        [Required(ErrorMessage = "Role Name is required.")]
        [StringLength(50, ErrorMessage = "Role Name cannot be more than 100 characters long.")]
        public string RoleName { get; set; }
        [Display(Name = "Role Description")]
        [StringLength(50, ErrorMessage = "Role Description cannot be more than 250 characters long.")]
        public string RoleDescription { get; set; }
        public DateTime RoleCreated { get; set; }
        public DateTime RoleUpdatedAt { get; set; }
        public ICollection<Account> Accounts { get; set; } = new HashSet<Account>();
    }
}
