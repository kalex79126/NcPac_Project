using System.ComponentModel.DataAnnotations;

namespace NcPac_Project2.Models.Enums
{
    public enum CommitteeRoleName
    {

        None,
        [Display(Name = "Chairperson")]
        Chair,

        [Display(Name = "Coordinator")]
        Coordinator,

        [Display(Name = "Secretary")]
        Secretary,

        [Display(Name = "Treasurer")]
        Treasurer,

        [Display(Name = "Deputy Chair")]
        DeputyChair,

        [Display(Name = "Member")]
        Member,

        [Display(Name = "Advisor")]
        Advisor
    }
}
