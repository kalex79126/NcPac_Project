
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NcPac_Project2.Models
{
    public class Member
    {
        public int MemberID { get; set; }

        [Display(Name = "Member")]
        public string MemFullName
        {
            get
            {
                return MemberFirstName + " " + MemberLastName;
            }
        }

        [Display(Name = "Phone")]
        public string MemPhoneFormat
        {
            get
            {
                return "(" + MemberPhoneNo.Substring(0, 3) + ") " + MemberPhoneNo.Substring(3, 3) + "-" + MemberPhoneNo[6..];
            }
        }

        [Display(Name = "Phone")]
        public string MemEmPhoneFormat
        {
            get
            {
                return "(" + MemberEmPhoneNo.Substring(0, 3) + ") " + MemberEmPhoneNo.Substring(3, 3) + "-" + MemberEmPhoneNo[6..];
            }
        }
        [Display(Name = "Salutation")]
        [StringLength(6, ErrorMessage = "Salutation cannot be more than 6 characters long.")]
        public string Salutation { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name of the Member is required.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string MemberFirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name of the Member is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot be more than 50 characters long.")]
        public string MemberLastName { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Living address of the Member is required")]
        [StringLength(50, ErrorMessage = "Address cannot be more than 50 characters long.")]
        public string MemberAddress { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City cannot be more than 50 characters long.")]
        public string MemberCity { get; set; }

        [Display(Name = "Province")]
        [Required(ErrorMessage = "Province is required.")]
        [StringLength(45, ErrorMessage = "Province cannot be more than 45 characters long.")]
        public string MemberProvince { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Postal code is required.")]
        [RegularExpression("^[ABCEGHJ-NPRSTVXY]\\d[ABCEGHJ-NPRSTV-Z][ ]\\d[ABCEGHJ-NPRSTV-Z]\\d$", ErrorMessage = "Please enter a valid Postal Code (X1X 1X1).")]
        [StringLength(7, ErrorMessage = "Postal code cannot be more than 7 characters long (including a space after 3 characters).")]
        public string MemberPostalCode { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Display(Name = "Phone")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        public string MemberPhoneNo { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Address is required.")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Please enter a valid Email Address.")]
        [StringLength(255, ErrorMessage = "Email Address cannot be more than 255 characters long.")]
        [DataType(DataType.EmailAddress)]
        public string MemberEmail { get; set; }

        [Display(Name = "Position")]
        [Required(ErrorMessage = "Current Employment Position is required.")]
        [StringLength(50, ErrorMessage = "Position cannot be more than 50 characters long.")]
        public string MemberPosition { get; set; }

        [Display(Name = "Company")]
        [Required(ErrorMessage = "Name of the Company currently employed at is required.")]
        [StringLength(50, ErrorMessage = "Company Name cannot be more than 50 characters long.")]
        public string MemberCompany { get; set; }


        [Display(Name = "Address")]
        [Required(ErrorMessage = "Employment Address is required")]
        [StringLength(50, ErrorMessage = "Employment Address cannot be more than 50 characters long.")]
        public string MemberEmAddress { get; set; }

        [Display(Name = "City")]
        [StringLength(50, ErrorMessage = "Employment City cannot be more than 50 characters long.")]
        public string MemberEmCity { get; set; }

        [Display(Name = "Province")]
        [StringLength(45, ErrorMessage = "Employment Province cannot be more than 45 characters long.")]
        public string MemberEmProvince { get; set; }

        [Display(Name = "Postal Code")]
        [RegularExpression("^[ABCEGHJ-NPRSTVXY]\\d[ABCEGHJ-NPRSTV-Z][ ]\\d[ABCEGHJ-NPRSTV-Z]\\d$", ErrorMessage = "Please enter a valid Postal Code (X1X 1X1).")]
        [Required(ErrorMessage = "Employment Postal code is required.")]
        [StringLength(7, ErrorMessage = "Employment Postal code cannot be more than 7 characters long (including a space after 3 characters).")]
        public string MemberEmPostalCode { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        public string MemberEmPhoneNo { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Employment Email Address is required.")]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Please enter a valid Email Address.")]
        [StringLength(255)]
        public string MemberEmEmail { get; set; }

        [Display(Name = "Preferred Email")]
        [Required(ErrorMessage = "Preferred Mailing Address is required.")]
        public string MemberIsMailingHome { get; set; }

        [Display(Name = "Niagara College Graduate")]
        [Required(ErrorMessage = "Niagara College Graduate is required, please select Yes or No.")]
        public string MemberIsNcGrad { get; set; }

        [Display(Name = "Education")]
        [Required(ErrorMessage = "Education is required.")]
        [StringLength(250, ErrorMessage = "Education description cannot be more than 250 characters long.")]
        public string MemberEducation { get; set; }

        [Display(Name = "Occupational Summary")]
        [StringLength(500, ErrorMessage = "Occupational Summary cannot be more than 500 characters long.")]
        public string MemberParticipation { get; set; }

        [Display(Name = "Registration Date")]
        [Required(ErrorMessage = "The Member Registration Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? MemberRegisteredAt { get; set; }

        [Display(Name = "Last Login")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? MemberLastLogin { get; set; }

        [Display(Name = "Renewal Date")]
        [Required(ErrorMessage = "The Member Renewal Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? MemberRenewalDate { get; set; }
        public int AccountID { get; set; }

        [ScaffoldColumn(false)]
        [Timestamp]
        public Byte[] RowVersion { get; set; }

        [Display(Name = "Committees")]
        public ICollection<MemberCommitteeEnroll> MemberCommitteeEnrolls { get; set; } = new HashSet<MemberCommitteeEnroll>();
        public ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();
        public ICollection<ActionItem> ActionItems { get; set; } = new HashSet<ActionItem>();
        public Account Account { get; set; }
        public ICollection<MemberDocument> MemberDocuments { get; set; } = new HashSet<MemberDocument>();


    }
}
