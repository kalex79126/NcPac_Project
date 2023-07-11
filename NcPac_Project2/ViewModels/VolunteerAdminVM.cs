using Microsoft.AspNetCore.Mvc;
using NcPac_Project2.Models.NotInDataModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NcPac_Project2.ViewModels
{
    /// <summary>
    /// Add back in any Restricted Properties and list of UserRoles
    /// </summary>
    [ModelMetadataType(typeof(VolunteerMetaData))]
    public class VolunteerAdminVM : VolunteerVM
    {
        public string Email { get; set; }
        public bool Active { get; set; }


        //[Display(Name = "Committeess")]
        //public List<string> CommitteeMember { get; set; } = new List<string>();

        [Display(Name = "Roles")]
        public List<string> UserRoles { get; set; } = new List<string>();
    }
}
