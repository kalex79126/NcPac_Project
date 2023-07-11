
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using NcPac_Project2.Models.Extra;

namespace NcPac_Project2.Models
{
    public class Meeting : Auditable
    {
        public int MeetingID { get; set; }

        public string MeetingAndDate
        {
            get
            {
                return MeetingTitle + " - " + MeetingDate;
            }
        }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "You cannot leave the Meeting Title blank.")]
        [StringLength(250, ErrorMessage = "Meeting Title cannot be more than 1000 characters long.")]
        public string MeetingTitle { get; set; }

        [Display(Name = "Date & Time")]
        [Required(ErrorMessage = "The Date & Time is required.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? MeetingDate { get; set; }

        //[Display(Name = "Time")]
        //[Required(ErrorMessage = "The Time is required.")]
        //[DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{hh:mm tt}", ApplyFormatInEditMode = true)]
        //public DateTime? MeetingTime{ get; set; }

        [Display(Name = "Notes")]
        [StringLength(1000, ErrorMessage = "Notes cannot be more than 1000 characters long.")]
        public string MeetingNotes { get; set; }

        [Display(Name = "Committee")]
        public int CommitteeID { get; set; }

        public Committee Committee { get; set; }

        [ScaffoldColumn(false)]
        [Timestamp]
        public Byte[] RowVersion { get; set; }
        public ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();
        public ICollection<CommitteeMeeting> CommitteeMeetings { get; set; } = new HashSet<CommitteeMeeting>();
        public ICollection<ActionItem> ActionItems { get; set; } = new HashSet<ActionItem>();


        [Display(Name = "Minutes")]
        public ICollection<MeetingDocument> MeetingMinutes  { get; set; } = new HashSet<MeetingDocument>();
      
    }
}
