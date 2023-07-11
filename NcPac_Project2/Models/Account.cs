using Microsoft.AspNetCore.Mvc;
using NcPac_Project2.Models.NotInDataModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NcPac_Project2.Models
{
    [ModelMetadataType(typeof(VolunteerMetaData))]
    /// <summary>
    /// Admin access only. Manage user accounts.
    /// </summary>
    public class Account
    {
        public int ID { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string FormalName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        public string PhoneNumber
        {
            get
            {
                if (String.IsNullOrEmpty(Phone))
                {
                    return "";
                }
                else
                {
                    return "(" + Phone.Substring(0, 3) + ") " + Phone.Substring(3, 3) + "-" + Phone.Substring(6, 4);
                }
            }
        }


        [Display(Name = "Salutation")]
        [Required(ErrorMessage = "Salutation is required.")]
        [StringLength(6, ErrorMessage = "Salutation cannot be more than 6 characters long.")]
        public string Salutaion { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        public string Phone{ get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(50, ErrorMessage = "Email cannot be more than 50 characters long.")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(255, ErrorMessage = "Last name cannot be more than 255 characters long.")]
        public string LastName { get; set; }
        public bool Active { get; set; } = true;
        public bool CompleteForm { get; set; } = true;
        public int RoleID { get; set; }
        public Role Role { get; set; }
        public Member Member { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
