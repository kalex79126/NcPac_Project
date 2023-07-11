using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NcPac_Project2.Models;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;

namespace NcPac_Project2.Data
{
    public static class NC_PAC_Initializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            NC_PAC_Context context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<NC_PAC_Context>();

            try
            {
                context.Database.Migrate();

                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                        new Role
                        {
                            RoleID = 1,
                            RoleName = "Top Admin",
                            RoleDescription = "Top Administrator of the Committee",
                            RoleCreated = DateTime.Parse("2023-01-15"),
                            RoleUpdatedAt = DateTime.Now
                        },
                        new Role
                        {
                            RoleID = 2,
                            RoleName = "Admin",
                            RoleDescription = "General Administrator of the Committee",
                            RoleCreated = DateTime.Parse("2023-01-15"),
                            RoleUpdatedAt = DateTime.Now
                        },
                        new Role
                        {
                            RoleID = 3,
                            RoleName = "Member",
                            RoleDescription = "General member of the Committee",
                            RoleCreated = DateTime.Parse("2023-01-15"),
                            RoleUpdatedAt = DateTime.Now
                        },
                        new Role
                        {
                            RoleID = 4,
                            RoleName = "Nc Staff",
                            RoleDescription = "Niagara College Staff of the Committee",
                            RoleCreated = DateTime.Parse("2023-01-15"),
                            RoleUpdatedAt = DateTime.Now
                        });
                    context.SaveChanges();
                }

                // Seed for admin accounts. Look for any VolunteerAdmin.  Seed ones to match the seeded Identity accounts.
                if (!context.Accounts.Any())
                {
                    context.Accounts.AddRange(
                     new Account
                     {
                         Salutaion = "Mr.",
                         Phone = "9055551212",
                         FirstName = "Roy",
                         LastName = "Matthews",
                         Email = "topadmin@outlook.com",
                         Active = true,
                         CompleteForm = true,
                         RoleID = 1
                     },
                     new Account
                     {
                         Salutaion = "Mr.",
                         Phone = "2893843943",
                         FirstName = "Greg",
                         LastName = "Kaiser",
                         Email = "admin@outlook.com",
                         Active = true,
                         CompleteForm = true,
                         RoleID = 2
                     },
                     new Account
                     {
                         Salutaion = "Mr.",
                         Phone = "3943849493",
                         FirstName = "Mark",
                         LastName = "Jones",
                         Email = "markjones@outlook.com",
                         Active = true,
                         CompleteForm = true,
                         RoleID = 3
                     },
                     new Account
                     {
                         Salutaion = "Mrs.",
                         Phone = "2893825566",
                         FirstName = "Dennis",
                         LastName = "Ward",
                         Email = "dennisward@outlook.com",
                         Active = true,
                         CompleteForm = true,
                         RoleID = 3
                     },
                     new Account
                     {
                         Salutaion = "Ms.",
                         Phone = "9057373434",
                         FirstName = "Alicia",
                         LastName = "Rumell",
                         Email = "aliciarumell@outlook.com",
                         Active = true,
                         CompleteForm = true,
                         RoleID = 3
                     },
                     new Account
                     {
                         Salutaion = "Mr.",
                         Phone = "5692155731",
                         FirstName = "Bob",
                         LastName = "Jackson",
                         Email = "bobjones@outlook.com",
                         Active = true,
                         CompleteForm = true,
                         RoleID = 3
                     },
                     new Account
                     {
                         Salutaion = "Miss.",
                         Phone = "9007543188",
                         FirstName = "Diana",
                         LastName = "Dymond",
                         Email = "dianadymond@outlook.com",
                         Active = true,
                         CompleteForm = true,
                         RoleID = 3
                     },
                     new Account
                     {
                         Salutaion = "Mr.",
                         Phone = "7542547889",
                         FirstName = "Carlos",
                         LastName = "Woods",
                         Email = "carloswoods@outlook.com",
                         Active = true,
                         CompleteForm = true,
                         RoleID = 3
                     },
                     new Account
                     {
                         Salutaion = "Mrs.",
                         Phone = "6158394849",
                         FirstName = "Kelly",
                         LastName = "Jonestone",
                         Email = "kellyjonestone@outlook.com",
                         Active = true,
                         CompleteForm = true,
                         RoleID = 3
                     },
                     new Account
                     {
                         Salutaion = "Dr.",
                         Phone = "2316938606",
                         FirstName = "Ryan",
                         LastName = "Murphy",
                         Email = "ryanmurphy@outlook.com",
                         Active = true,
                         CompleteForm = true,
                         RoleID = 3
                     });
                    context.SaveChanges();
                }
                if (!context.Divisions.Any())
                {
                    context.Divisions.AddRange(
                    new Division
                    {
                        DivisionTitle = "Media, Trades & Technology",
                    },
                    new Division
                    {
                        DivisionTitle = "Community & Health Studies",
                    },
                    new Division
                    {
                        DivisionTitle = "Academic & Liberal Studies",
                    },
                    new Division
                    {
                        DivisionTitle = "Business, Hospitality & Environment",
                    },
                    new Division
                    {
                        DivisionTitle = "Canadian Food & Wine Institute",
                    });
                    context.SaveChanges();
                }
                if (!context.Committees.Any())
                {
                    context.Committees.AddRange(
                     new Committee
                     {
                         CommitteeName = "Acting",
                         DivisionID = 1
                     },
                        new Committee
                        {
                            CommitteeName = "Broadcasting Radio Television & Film",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Computer Programming & Computer Programming Analysis",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Game Development",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Photography",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Public Relations",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Social Media Management",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Computer Systems Technician",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Computer, Electrical & Electronics Engineering",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Construction & Civil Engineering",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Industrial Automation",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Mechanical Engineering",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Photonics Engineering",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Renewable Energies",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Carpentry & Renovation",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Electrical Techniques",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Hairstyling",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Mechanical  Techniques",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Motive Power",
                            DivisionID = 1
                        },
                        new Committee
                        {
                            CommitteeName = "Welding Skills",
                            DivisionID = 1
                        });
                    context.SaveChanges();
                }

                if (!context.Meetings.Any())
                {
                    context.Meetings.AddRange(
                    new Meeting
                    {
                        MeetingTitle = "January 2023 Acting Programs Advisory Committee Meeting",
                        MeetingNotes = "Acting Programs Advisory Committee Meeting",
                        MeetingDate = DateTime.Parse("2023-04-28 09:30 AM"),
                    },
                    new Meeting
                    {
                        MeetingTitle = "February 2023 Broadcasting Radio Television & Film Programs Advisory Committee Meeting",
                        MeetingNotes = "Broadcasting Radio Television & Film Programs Advisory Committee Meeting",
                        MeetingDate = DateTime.Parse("2023-04-25 01:30 PM"),
                    },
                    new Meeting
                    {
                        MeetingTitle = "August 2023 Computer Programming & Computer Programming Analysis Programs Advisory Committee Meeting",
                        MeetingNotes = "Computer Programming & Computer Programming Analysis Programs Advisory Committee Meeting",
                        MeetingDate = DateTime.Parse("2023-03-14 02:30 PM"),
                    },
                    new Meeting
                    {
                        MeetingTitle = "May 2023 Game Development Programs Advisory Committee Meeting",
                        MeetingNotes = "Game Development Programs Advisory Committee Meeting",
                        MeetingDate = DateTime.Parse("2023-04-20 10:30 AM"),
                    },
                    new Meeting
                    {
                        MeetingTitle = "September 2023 Photography Programs Advisory Committee Meeting",
                        MeetingNotes = "Photography Programs Advisory Committee Meeting",
                        MeetingDate = DateTime.Parse("2023-03-14 11:30 AM"),
                    },
                    new Meeting
                    {
                        MeetingTitle = "December 2022 Public Relations Programs Advisory Committee Meeting",
                        MeetingNotes = "Public Relations Programs Advisory Committee Meeting",
                        MeetingDate = DateTime.Parse("2022-03-12 10:00 AM"),
                    },
                    new Meeting
                    {
                        MeetingTitle = "January 2023 Social Media Management Programs Advisory Committee Meeting",
                        MeetingNotes = "Social Media Management Programs Advisory Committee Meeting",
                        MeetingDate = DateTime.Parse("2023-03-01 03:30 PM"),
                    },
                    new Meeting
                    {
                        MeetingTitle = "July 2023 Computer Systems Technician Programs Advisory Committee Meeting",
                        MeetingNotes = "Computer Systems Technician Programs Advisory Committee Meeting",
                        MeetingDate = DateTime.Parse("2023-03-11 03:00 PM"),
                    },
                    new Meeting
                    {
                        MeetingTitle = "January 2023 Computer, Electrical & Electronics Engineering Programs Advisory Committee Meeting",
                        MeetingNotes = "Game Development Programs Advisory Committee Meeting",
                        MeetingDate = DateTime.Parse("2023-05-01 12:30 PM"),
                    },
                    new Meeting
                    {
                        MeetingTitle = "October 2022 Construction & Civil Engineering Programs Advisory Committee Meeting",
                        MeetingNotes = "Construction & Civil Engineering Programs Advisory Committee Meeting",
                        MeetingDate = DateTime.Parse("2022-05-28 01:00 PM"),
                    });
                    context.SaveChanges();
                }

                if (!context.CommitteeMeetings.Any())
                {
                    context.CommitteeMeetings.AddRange(
                        new CommitteeMeeting
                        {
                            CommitteeID = 1,
                            MeetingID = 1,
                        },
                        new CommitteeMeeting
                        {
                            CommitteeID = 2,
                            MeetingID = 2,
                        },
                        new CommitteeMeeting
                        {
                            CommitteeID = 3,
                            MeetingID = 3,
                        },
                        new CommitteeMeeting
                        {
                            CommitteeID = 4,
                            MeetingID = 4,
                        },
                        new CommitteeMeeting
                        {
                            CommitteeID = 5,
                            MeetingID = 5,
                        },
                        new CommitteeMeeting
                        {
                            CommitteeID = 6,
                            MeetingID = 6,
                        },
                        new CommitteeMeeting
                        {
                            CommitteeID = 7,
                            MeetingID = 7,
                        },
                        new CommitteeMeeting
                        {
                            CommitteeID = 8,
                            MeetingID = 8,
                        },
                        new CommitteeMeeting
                        {
                            CommitteeID = 9,
                            MeetingID = 9,
                        },
                        new CommitteeMeeting
                        {
                            CommitteeID = 10,
                            MeetingID = 10,
                        });
                    context.SaveChanges();
                }

                if (!context.CommitteeRoles.Any())
                {
                    context.CommitteeRoles.AddRange(
                    new CommitteeRole
                    {
                        RoleName = "Chairperson",
                        RoleDescription = "Charing committee meetings and acting as spokeperson"
                    },
                    new CommitteeRole
                    {
                        RoleName = "Coordinator",
                        RoleDescription = "Preparing and distributing meeting agendas and minutes in coordination"
                    },
                    new CommitteeRole
                    {
                        RoleName = "Secretary",
                        RoleDescription = "Arranging meetings and taking minutes"
                    },
                    new CommitteeRole
                    {
                        RoleName = "Treasurer",
                        RoleDescription = "Manage income and expenditure"
                    },
                    new CommitteeRole
                    {
                        RoleName = "Deputy Chair",
                        RoleDescription = "Shadow the Chairperson"
                    });
                    context.SaveChanges();
                }

                if (!context.Members.Any())
                {
                    context.Members.AddRange(
                    new Member
                    {
                        Salutation = "Mr.",
                        MemberFirstName = "Roy",
                        MemberLastName = "Matthews",
                        MemberAddress = "123 Niagara Street",
                        MemberCity = "St.Catharines",
                        MemberProvince = "Ontario",
                        MemberPostalCode = "X2H 9A2",
                        MemberPhoneNo = "9055551212",
                        MemberEmail = "MatthewsRoy@outlook.com",
                        MemberPosition = "Engineering Technologist",
                        MemberCompany = "Easy Tech.",
                        MemberEmAddress = "31 King Street",
                        MemberEmCity = "Toronto",
                        MemberEmProvince = "Ontario",
                        MemberEmPostalCode = "B2E 3S4",
                        MemberEmPhoneNo = "8802333424",
                        MemberEmEmail = "Matthewsroy@easytech.com",
                        MemberIsMailingHome = "Work",
                        MemberIsNcGrad = "Yes",
                        MemberEducation = "Bachelors Degree in Software Engineering at University of Waterloo, Degree in Computer Programming at Niagara College",
                        MemberParticipation = "",
                        MemberRegisteredAt = DateTime.Parse("2020-04-20"),
                        MemberLastLogin = DateTime.Parse("2023-01-15"),
                        MemberRenewalDate = DateTime.Parse("2023-04-20"),
                        AccountID = 1
                    },
                    new Member
                    {
                        Salutation = "Mr.",
                        MemberFirstName = "Greg",
                        MemberLastName = "Kaiser",
                        MemberAddress = "813 St. Laurent Boulevard",
                        MemberCity = "Ottawa",
                        MemberProvince = "Ontario",
                        MemberPostalCode = "E3C 0Z2",
                        MemberPhoneNo = "2893843943",
                        MemberEmail = "KaiserGreg@gmail.com",
                        MemberPosition = "Director of Preconstruction Services",
                        MemberCompany = "AtoZ Real Estate Brokerage",
                        MemberEmAddress = "13 First Street",
                        MemberEmCity = "Ottawa",
                        MemberEmProvince = "Ontario",
                        MemberEmPostalCode = "J3E 5H3",
                        MemberEmPhoneNo = "8804358734",
                        MemberEmEmail = "KaiserGreg@atozrealestate.com",
                        MemberIsMailingHome = "Personal",
                        MemberIsNcGrad = "Yes",
                        MemberEducation = "Bachelors Degree in Bussiness at Western University, Degree in Sales and Marketing at Niagara College",
                        MemberParticipation = "",
                        MemberRegisteredAt = DateTime.Parse("2020-04-23"),
                        MemberLastLogin = DateTime.Parse("2023-01-15"),
                        MemberRenewalDate = DateTime.Parse("2023-04-23"),
                        AccountID = 2
                    },
                    new Member
                    {
                        Salutation = "Mr.",
                        MemberFirstName = "Mark",
                        MemberLastName = "Jones",
                        MemberAddress = "11 John Street",
                        MemberCity = "Montreal",
                        MemberProvince = "Quebec",
                        MemberPostalCode = "L4E 4Z8",
                        MemberPhoneNo = "3943849493",
                        MemberEmail = "JonesMark@outlook.com",
                        MemberPosition = "Senior Project Engineer",
                        MemberCompany = "Network Solutions INC.",
                        MemberEmAddress = "131 Greene Avenue",
                        MemberEmCity = "Montreal",
                        MemberEmProvince = "Quebec",
                        MemberEmPostalCode = "L4E 6Z7",
                        MemberEmPhoneNo = "8003843842",
                        MemberEmEmail = "Jonesmark@networksolutions.com",
                        MemberIsMailingHome = "Work",
                        MemberIsNcGrad = "Yes",
                        MemberEducation = "Master's Degree in Computer Programming at McGill University, Degree in Computer Systems Technitian at Niagara College",
                        MemberParticipation = "",
                        MemberRegisteredAt = DateTime.Parse("2020-05-23"),
                        MemberLastLogin = DateTime.Parse("2021-04-17"),
                        MemberRenewalDate = DateTime.Parse("2023-05-23"),
                        AccountID = 3
                    },
                    new Member
                    {
                        Salutation = "Mrs.",
                        MemberFirstName = "Dennis",
                        MemberLastName = "Ward",
                        MemberAddress = "9913 Dundas Street",
                        MemberCity = "Toronto",
                        MemberProvince = "Ontario",
                        MemberPostalCode = "B1A 8N3",
                        MemberPhoneNo = "2893825566",
                        MemberEmail = "WardDennis@outlook.com",
                        MemberPosition = "Sr. Vice President",
                        MemberCompany = "Reveal Studio",
                        MemberEmAddress = "131 Younge Street",
                        MemberEmCity = "Toronto",
                        MemberEmProvince = "Ontario",
                        MemberEmPostalCode = "L1E 4N7",
                        MemberEmPhoneNo = "8809394939",
                        MemberEmEmail = "Warddennis@reveal.com",
                        MemberIsMailingHome = "Personal",
                        MemberIsNcGrad = "No",
                        MemberEducation = "Bachelors Degree in Graphic Designing at OCAD University",
                        MemberParticipation = "",
                        MemberRegisteredAt = DateTime.Parse("2020-06-23"),
                        MemberLastLogin = DateTime.Parse("2022-04-29"),
                        MemberRenewalDate = DateTime.Parse("2023-06-23"),
                        AccountID = 4
                    },
                    new Member
                    {
                        Salutation = "Ms.",
                        MemberFirstName = "Alicia",
                        MemberLastName = "Rumell",
                        MemberAddress = "213 Paul Street",
                        MemberCity = "Welland",
                        MemberProvince = "Ontario",
                        MemberPostalCode = "L3R 7R3",
                        MemberPhoneNo = "9057373434",
                        MemberEmail = "RumellAlicia@outlook.com",
                        MemberPosition = "Chief Estimating Officer/Partner",
                        MemberCompany = "ERD Group",
                        MemberEmAddress = "31 Lake Street",
                        MemberEmCity = "St.Catharines",
                        MemberEmProvince = "Ontario",
                        MemberEmPostalCode = "L1E 3Z7",
                        MemberEmPhoneNo = "8009098766",
                        MemberEmEmail = "Rumellalicia@erd.com",
                        MemberIsMailingHome = "Work",
                        MemberIsNcGrad = "Yes",
                        MemberEducation = "Bachelors Degree in Accouting at Brock University, Degree in Accounting at Niagara College",
                        MemberParticipation = "",
                        MemberRegisteredAt = DateTime.Parse("2020-09-23"),
                        MemberLastLogin = DateTime.Parse("2022-03-14"),
                        MemberRenewalDate = DateTime.Parse("2020-09-23"),
                        AccountID = 5
                    },
                        new Member
                        {
                            Salutation = "Mr.",
                            MemberFirstName = "Bob",
                            MemberLastName = "Jackson",
                            MemberAddress = "372 Gonville Road",
                            MemberCity = "Jordan",
                            MemberProvince = "Ontario",
                            MemberPostalCode = "L6C 6Z4",
                            MemberPhoneNo = "5692155731",
                            MemberEmail = "BobJackson348@outlook.com",
                            MemberPosition = "Senior Graphic Programmer",
                            MemberCompany = "Blizzard Gaming",
                            MemberEmAddress = "31 Lake Street",
                            MemberEmCity = "Fort George",
                            MemberEmProvince = "Ontario",
                            MemberEmPostalCode = "E5E 1T8",
                            MemberEmPhoneNo = "8140352438",
                            MemberEmEmail = "Bobjackson.blizzardgaming@gmail.com",
                            MemberIsMailingHome = "Personal",
                            MemberIsNcGrad = "Yes",
                            MemberEducation = "Bachelors Degree in Computer Science at University of Toronto, Degree in Game Development at Niagara College",
                            MemberParticipation = "",
                            MemberRegisteredAt = DateTime.Parse("2021-07-31"),
                            MemberLastLogin = DateTime.Parse("2023-03-11"),
                            MemberRenewalDate = DateTime.Parse("2024-07-31"),
                            AccountID = 6
                        },
                        new Member
                        {
                            Salutation = "Miss",
                            MemberFirstName = "Diana",
                            MemberLastName = "Dymond",
                            MemberAddress = "3893 Tesla Boulevard",
                            MemberCity = "Hamilton",
                            MemberProvince = "Ontario",
                            MemberPostalCode = "E8B 9N9",
                            MemberPhoneNo = "9007543188",
                            MemberEmail = "DDymond01@outlook.com",
                            MemberPosition = "Data Analyst",
                            MemberCompany = "AMS Cloud",
                            MemberEmAddress = "984 Valley Way",
                            MemberEmCity = "Niagara-on-the-lake",
                            MemberEmProvince = "Ontario",
                            MemberEmPostalCode = "S9H 9H0",
                            MemberEmPhoneNo = "4564947617",
                            MemberEmEmail = "Ddymond01.AMSCloud@easytech.com",
                            MemberIsMailingHome = "Personal",
                            MemberIsNcGrad = "Yes",
                            MemberEducation = "Bachelors Degree in Software Engineering at University of Waterloo, Degree in Computer Programming at Niagara College",
                            MemberParticipation = "",
                            MemberRegisteredAt = DateTime.Parse("2022-11-30"),
                            MemberLastLogin = DateTime.Parse("2023-02-27"),
                            MemberRenewalDate = DateTime.Parse("2025-11-30"),
                            AccountID = 7
                        },
                        new Member
                        {
                            Salutation = "Mr.",
                            MemberFirstName = "Carlos",
                            MemberLastName = "Woods",
                            MemberAddress = "2883 Church Street",
                            MemberCity = "Oakville",
                            MemberProvince = "Ontario",
                            MemberPostalCode = "A2N 5G2",
                            MemberPhoneNo = "7542547889",
                            MemberEmail = "CWoods1@gamil.com",
                            MemberPosition = "Professor",
                            MemberCompany = "Niagara College",
                            MemberEmAddress = "100 Niagara College Boulevard",
                            MemberEmCity = "Welland",
                            MemberEmProvince = "Ontario",
                            MemberEmPostalCode = "L3C 7Z3",
                            MemberEmPhoneNo = "3799422066",
                            MemberEmEmail = "Cwoods1@niagaracollege.ncprof.ca",
                            MemberIsMailingHome = "Personal",
                            MemberIsNcGrad = "No",
                            MemberEducation = "PhD in Accountingat University of British Comlombia",
                            MemberParticipation = "",
                            MemberRegisteredAt = DateTime.Parse("2020-01-27"),
                            MemberLastLogin = DateTime.Parse("2023-01-28"),
                            MemberRenewalDate = DateTime.Parse("2023-01-27"),
                            AccountID = 8
                        },
                        new Member
                        {
                            Salutation = "Mrs.",
                            MemberFirstName = "Kelly",
                            MemberLastName = "Johnstone",
                            MemberAddress = "3874 Ivory Road",
                            MemberCity = "Welland",
                            MemberProvince = "Ontario",
                            MemberPostalCode = "G5H 2N1",
                            MemberPhoneNo = "6158394849",
                            MemberEmail = "KellyJohnstone@outlook.com",
                            MemberPosition = "CEO",
                            MemberCompany = "Dream Photos",
                            MemberEmAddress = "37432 Younge Street",
                            MemberEmCity = "Toronto",
                            MemberEmProvince = "Ontario",
                            MemberEmPostalCode = "G5T 4N7",
                            MemberEmPhoneNo = "1647850368",
                            MemberEmEmail = "kjohnstone1@dreamphotos.com",
                            MemberIsMailingHome = "Work",
                            MemberIsNcGrad = "No",
                            MemberEducation = "Bachelors Degree in Digitial Photography at York University",
                            MemberParticipation = "",
                            MemberRegisteredAt = DateTime.Parse("2023-01-03"),
                            MemberLastLogin = DateTime.Parse("2023-02-23"),
                            MemberRenewalDate = DateTime.Parse("2026-01-03"),
                            AccountID = 9
                        },
                        new Member
                        {
                            Salutation = "Dr.",
                            MemberFirstName = "Ryan",
                            MemberLastName = "Murphy",
                            MemberAddress = "7634 Lumber Lane",
                            MemberCity = "Missisauga",
                            MemberProvince = "Ontario",
                            MemberPostalCode = "T5A 1H2",
                            MemberPhoneNo = "2316938606",
                            MemberEmail = "RyanMurphy@outlook.com",
                            MemberPosition = "Dentist",
                            MemberCompany = "Doctor Murphy Dentist",
                            MemberEmAddress = "76 Main Street",
                            MemberEmCity = "Missisauga",
                            MemberEmProvince = "Ontario",
                            MemberEmPostalCode = "G3H 1N8",
                            MemberEmPhoneNo = "8532210794",
                            MemberEmEmail = "Ryanmurphy.murphydentist@outlook.com",
                            MemberIsMailingHome = "Work",
                            MemberIsNcGrad = "No",
                            MemberEducation = "PhD in Dental school at University of Toronto",
                            MemberParticipation = "",
                            MemberRegisteredAt = DateTime.Parse("2020-10-28"),
                            MemberLastLogin = DateTime.Parse("2023-03-01"),
                            MemberRenewalDate = DateTime.Parse("2023-10-28"),
                            AccountID = 10
                        });
                    context.SaveChanges();
                }
              
                if (!context.ActionItems.Any())
                {
                    context.ActionItems.AddRange(
                    new ActionItem
                    {
                        MeetingID = 1,
                        MemberID = 1,
                        ActionName = "Meeting Arrangement",
                        ActionDescription = "Arrange the meeting and notify to Members",
                        ActionDueDate = DateTime.Parse("2023-04-20"),
                        IsCompleted = false
                    },
                    new ActionItem
                    {
                        MeetingID = 2,
                        MemberID = 2,
                        ActionName = "Financial Management",
                        ActionDescription = "Manages financial transactions of the committee",
                        ActionDueDate = DateTime.Parse("2023-04-28"),
                        IsCompleted = true
                    },
                    new ActionItem
                    {
                        MeetingID = 3,
                        MemberID = 3,
                        ActionName = "Minutes Record",
                        ActionDescription = "Take minutes of a meeting",
                        ActionDueDate = DateTime.Parse("2023-04-23"),
                        IsCompleted= true
                    },
                    new ActionItem
                    {
                        MeetingID = 4,
                        MemberID = 4,
                        ActionName = "Meeting Preparement",
                        ActionDescription = "Prepare required materials for a meeting",
                        ActionDueDate = DateTime.Parse("2023-03-15"),
                        IsCompleted = false
                    },
                    new ActionItem
                    {
                        MeetingID = 5,
                        MemberID = 5,
                        ActionName = "Announce Updates",
                        ActionDescription = "Announce the updates to the committee",
                        ActionDueDate = DateTime.Parse("2023-04-25"),
                        IsCompleted = false
                    });
                    context.SaveChanges();
                }

                Random random = new(99);
                int[] memberIDs = context.Members.Select(s => s.MemberID).ToArray();
                int memberIDCount = memberIDs.Length;
                int[] CommitteeRoleIDs = context.CommitteeRoles.Select(c => c.CommitteeRoleID).ToArray();
                int CommitteeRoleIDCount = CommitteeRoleIDs.Length;
                int[] committeeIDs = context.Committees.Select(s => s.CommitteeID).ToArray();
                int committeeIDCount = committeeIDs.Length;
                if (!context.MemberCommitteeEnrolls.Any())
                {
                    int k = 0;
                    foreach (int t in memberIDs)
                    {
                        int howMany = random.Next(1, 3);
                        for (int j = 1; j <= howMany; j++)
                        {
                            k = (k >= committeeIDCount) ? 0 : k;
                            int randomCommitteeRoleIDIndex = random.Next(CommitteeRoleIDCount);
                            MemberCommitteeEnroll e = new MemberCommitteeEnroll()
                            {
                                MemberID = t,
                                CommitteeID = committeeIDs[k],
                                CommitteeRoleID = CommitteeRoleIDs[randomCommitteeRoleIDIndex]
                            };
                            k++;
                            context.MemberCommitteeEnrolls.Add(e);
                        }
                        context.SaveChanges();
                    }
                }

                if (!context.Attendances.Any())
                {
                    context.Attendances.AddRange(
                    new Attendance
                    {
                        MeetingID = 1, //Roy or Mathews
                        MemberID = 1,
                        AttendanceFirstRecent = true,
                        AttendanceSecondRecent = false,
                        AttendanceThirdRecent = true
                    },
                    new Attendance
                    {
                        MeetingID = 1,
                        MemberID = 2,
                        AttendanceFirstRecent = false,
                        AttendanceSecondRecent = false,
                        AttendanceThirdRecent = false
                    },
                    new Attendance
                    {
                        MeetingID = 1,
                        MemberID = 3,
                        AttendanceFirstRecent = true,
                        AttendanceSecondRecent = true,
                        AttendanceThirdRecent = true
                    },
                    new Attendance
                    {
                        MeetingID = 1,
                        MemberID = 4,
                        AttendanceFirstRecent = false,
                        AttendanceSecondRecent = false,
                        AttendanceThirdRecent = true
                    },
                    new Attendance
                    {
                        MeetingID = 1,
                        MemberID = 5,
                        AttendanceFirstRecent = true,
                        AttendanceSecondRecent = true,
                        AttendanceThirdRecent = false
                    });
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}