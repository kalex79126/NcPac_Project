using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NcPac_Project2.Models.Enums
{
    /// <summary>
    /// Enum for list of provinces in Register Page
    /// </summary>
    public enum Provinces
    {

        [Display(Name = "Alberta")]
        AB,
        [Display(Name = "British Columbia")]
        BC,
        [Display(Name = "Manitoba")]
        MB,
        [Display(Name = "New Brrunswick")]
        NB,
        [Display(Name = "Newfoundland and Labrador")]
        NL,
        [Display(Name = "Northwest Territories")]
        NT,
        [Display(Name = "Nova Scotia")]
        NS,
        [Display(Name = "Nunavut")]
        NU,
        [Display(Name = "Ontario")]
        ON,
        [Display(Name = "Quebec")]
        QC,
        [Display(Name = "Prince Edward Island")]
        PE,
        [Display(Name = "Saskatchewan")]
        SK,
        [Display(Name = "Yukon")]
        YT
    }
}
