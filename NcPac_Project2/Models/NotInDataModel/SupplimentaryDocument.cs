using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using NcPac_Project2.Models.Auditing;

namespace NcPac_Project2.Models.NotInDataModel
{
    public class SupplimentaryDocument : UploadedFile
    {
        [Display(Name = "Action Item")]
        public int TaskID { get; set; }
        public ActionItem ActionItem { get; set; }

        [Display(Name = "Description")]
        [StringLength(1000, ErrorMessage = "Description should be less than 1000 characters in length.")]
        public string Description { get; set; }
    }
}
