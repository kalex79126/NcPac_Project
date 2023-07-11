using System.ComponentModel.DataAnnotations;
using NcPac_Project2.Models.Auditing;

namespace NcPac_Project2.Models
{
    public class MeetingDocument : UploadedFile
    {
        [Display(Name = "Meeting")]
        public int MeetingID { get; set; }
        public Meeting Meeting { get; set; }

        [Display(Name = "Description")]
        [StringLength(1000, ErrorMessage = "Description should be less than 1000 characters in length.")]
        public string Description { get; set; }
    }
}
