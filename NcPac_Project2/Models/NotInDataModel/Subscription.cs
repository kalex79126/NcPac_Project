using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NcPac_Project2.Models.NotInDataModel
{
    public class Subscription
    {
        public int Id { get; set; }

        [StringLength(512)]
        public string PushEndpoint { get; set; }

        [StringLength(512)]
        public string PushP256DH { get; set; }

        [StringLength(512)]
        public string PushAuth { get; set; }

        [Required(ErrorMessage = "You must select the Staff or Member.")]
        [Display(Name = "Member or Staff")]
        public int VolunteerID { get; set; }
        public Account Volunteer { get; set; }
    }
}
