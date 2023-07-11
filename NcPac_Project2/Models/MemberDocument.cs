using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using NcPac_Project2.Models.Auditing;

namespace NcPac_Project2.Models
{
    public class MemberDocument : UploadedFile
    {
        [Display(Name = "Member")]
        public int MemberID { get; set; }

        public Member Member { get; set; }
    }
}
