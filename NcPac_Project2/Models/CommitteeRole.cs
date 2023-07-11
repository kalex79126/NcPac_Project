
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NcPac_Project2.Models
{
    public class CommitteeRole
    {
        [Key]
        public int CommitteeRoleID { get; set; }

        [Required(ErrorMessage = "You cannot leave the Role Name blank.")]
        [StringLength(40, ErrorMessage = "Role Name cannot be more than 40 characters long.")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "You cannot leave the Role Description blank.")]
        [StringLength(40, ErrorMessage = "Role Description cannot be more than 40 characters long.")]
        public string RoleDescription { get; set; }

        public ICollection<MemberCommitteeEnroll> MemberCommitteeEnrolls { get; set; } = new HashSet<MemberCommitteeEnroll>();

    }
}
