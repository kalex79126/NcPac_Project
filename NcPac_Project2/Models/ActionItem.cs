
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using NcPac_Project2.Models.Extra;
using NcPac_Project2.Models.NotInDataModel;

namespace NcPac_Project2.Models
{
    public class ActionItem : Auditable
    {
        [Key]
        public int ActionID { get; set; }

        [Display(Name = "Action Item Name")]
        [Required(ErrorMessage = "You cannot leave the Action Item blank.")]
        [StringLength(250, ErrorMessage = "Action Item cannot be more than 250 characters long.")]
        public string ActionName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "You cannot leave the Action Item Description blank.")]
        [StringLength(250, ErrorMessage = "Role Description cannot be more than 250 characters long.")]
        public string ActionDescription { get; set; }

        [Display(Name = "Due Date")]
        [Required(ErrorMessage = "The Action Item Due Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ActionDueDate { get; set; }

        [Display(Name = "Notes")]
        [StringLength(500, ErrorMessage = "Notes cannot be more than 500 characters long.")]
        public string ActionNotes { get; set; }

        [Display(Name = "Item Completed?")]
        [Required(ErrorMessage = "Action Item Status is required.")]
        public bool IsCompleted { get; set; }

        [ScaffoldColumn(false)]
        [Timestamp]
        public Byte[] RowVersion { get; set; }

        [Display(Name = "Meeting")]
        public int MeetingID { get; set; }

        [Display(Name = "Member")]
        public int MemberID { get; set; }
        
        public Member Member { get; set; }
        public Meeting Meeting { get; set; }

        [Display(Name = "Supplementary Document")]
        public ICollection<SupplimentaryDocument> SupplimentaryDocuments { get; set; } = new HashSet<SupplimentaryDocument>();
    }
}
