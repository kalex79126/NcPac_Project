using System.ComponentModel.DataAnnotations;

namespace NcPac_Project2.Models.Enums
{
    public enum Committees
    {

        None,
        [Display(Name = "Acting")]
        Acting,

        [Display(Name = "Broadcasting Radio Television & Film")]
        BroadcastingRadioTelevisionFilm,

        [Display(Name = "Computer Programming & Computer Programming Analysis")]
        ComputerProgrammingComputerProgrammingAnalysis,

        [Display(Name = "Game Development")]
        GameDevelopment,

        [Display(Name = "Graphic Design")]
        GraphicDesign,

        [Display(Name = "Photography")]
        Photography,

        [Display(Name = "Public Relations")]
        PublicRelations,

        [Display(Name = "Social Media Management")]
        SocialMediaManagement,

        [Display(Name = "ComputerSystemsTechnician")]
        ComputerSystemsTechnician,

        [Display(Name = "Computer, Electrical & Electronics Engineering")]
        ComputerElectricalElectronicsEngineering,

        [Display(Name = "Construction & Civil Engineering")]
        ConstructionCiviEngineering,

        [Display(Name = "Industrial Automation")]
        IndustrialAutomation,

        [Display(Name = "Mechanical Engineering")]
        MechanicalEngineering,

        [Display(Name = "Photonics Engineering")]
        PhotonicsEngineering,

        [Display(Name = "Renewable Energies")]
        RenewableEnergies,

        [Display(Name = "Carpentry & Renovation")]
        CarpentryRenovation,

        [Display(Name = "Electrical Techniques")]
        ElectricalTechniques,

        [Display(Name = "Hairstyling")]
        Hairstyling,

        [Display(Name = "Mechanical Techniques")]
        MechanicalTechniques,

        [Display(Name = "Motive Power")]
        MotivePower,

        [Display(Name = "Welding Skills")]
        WeldingSkills

    }
}
