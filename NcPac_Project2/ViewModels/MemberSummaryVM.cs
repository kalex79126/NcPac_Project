using NcPac_Project2.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NcPac_Project2.ViewModels
{
    /// <summary>
    ///Report to select multiple options from Members by each PAC, city, NC grad, 
    ///appointed date, renewed date, retirement date, number of years on PAC, PAC chairs, 
    ///by addresses, email addresses, phonenumber, job title, company
    /// </summary>

    public class MemberSummaryVM
    {
        public int ID { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? " " :
                        (" " + (char?)MiddleName[0] + ". ").ToUpper())
                    + LastName;
            }
        }

        [Display(Name ="Years on Pac")]
        public int PacYears
        {
            get
            {
                return Convert.ToInt32((RenewedDate.Value.Year - RegisteredAt.Value.Year));
            }
        }

        [Display(Name = "Phone")]
        public string PhoneFormat
        {
            get
            {
                return "(" + Phone.Substring(0, 3) + ") " + Phone.Substring(3, 3) + "-" + Phone[6..];
            }
        }

        //Two months before the RenewalDate to give the user a reminder of this appointment
        [Display(Name ="Appointment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime GetAppointedDate
        {
            get
            {
                return RenewedDate.Value.AddMonths(-2);
            }
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        [Display(Name ="Job Title")]
        public string JobTitle { get; set; }

        public string Company { get; set; }

        public string NcGrad { get; set; }

        [Display(Name ="Appointment Date")]
        public DateTime AppointedDate { get; set; }

        [Display(Name = "Renewal")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? RenewedDate { get; set; }

        //public DateTime? RetirementDate { get; set; }
        
        [Display(Name = "Registration")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? RegisteredAt { get; set; }
        
        [Display(Name = "PAC Years")]
        public int NumberOfYearsPac { get; set; }

        [Display(Name ="Chairs")]
        public string PacChairs { get; set; }

        [Display(Name = "Committee")]
        public string CommitteeName { get; set; }

        [Display(Name = "Division")]
        public string DivisionName { get; set; }

        [Display(Name = "Committees")]
        public ICollection<MemberCommitteeEnroll> MemberCommitteeEnrolls { get; set; } = new HashSet<MemberCommitteeEnroll>();

        [Display(Name = "Attendances")]
        public ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();

        //Get Last Attendance
        [Display(Name ="Last Attendance")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LastAttendanceDate { get; set; }
    }
}
