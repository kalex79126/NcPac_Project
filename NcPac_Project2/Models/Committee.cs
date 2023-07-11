using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NcPac_Project2.Models
{
    public class Committee
    {
        public int CommitteeID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "You cannot leave the committee Name blank.")]
        [StringLength(100, ErrorMessage = "Committee Name cannot be more than 50 characters long.")]
        public string CommitteeName { get; set; }

        [Display(Name = "Division")]
        public int DivisionID { get; set; }

        public Division Division { get; set; }

        public ICollection<MemberCommitteeEnroll> MemberCommitteeEnrolls { get; set; } = new HashSet<MemberCommitteeEnroll>();
        public ICollection<CommitteeMeeting> CommitteeMeetings { get; set; } = new HashSet<CommitteeMeeting>();
    }
}
