using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NcPac_Project2.Data.NC_PAC_Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommitteeRoles",
                columns: table => new
                {
                    CommitteeRoleID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    RoleDescription = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitteeRoles", x => x.CommitteeRoleID);
                });

            migrationBuilder.CreateTable(
                name: "Divisions",
                columns: table => new
                {
                    DivisionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DivisionTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisions", x => x.DivisionID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    RoleDescription = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    RoleCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RoleUpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Committees",
                columns: table => new
                {
                    CommitteeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommitteeName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DivisionID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Committees", x => x.CommitteeID);
                    table.ForeignKey(
                        name: "FK_Committees_Divisions_DivisionID",
                        column: x => x.DivisionID,
                        principalTable: "Divisions",
                        principalColumn: "DivisionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Salutaion = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CompleteForm = table.Column<bool>(type: "INTEGER", nullable: false),
                    RoleID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    MeetingID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MeetingTitle = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    MeetingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MeetingNotes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CommitteeID = table.Column<int>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.MeetingID);
                    table.ForeignKey(
                        name: "FK_Meetings_Committees_CommitteeID",
                        column: x => x.CommitteeID,
                        principalTable: "Committees",
                        principalColumn: "CommitteeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Salutation = table.Column<string>(type: "TEXT", maxLength: 6, nullable: true),
                    MemberFirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MemberLastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MemberAddress = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MemberCity = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MemberProvince = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    MemberPostalCode = table.Column<string>(type: "TEXT", maxLength: 7, nullable: false),
                    MemberPhoneNo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    MemberEmail = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    MemberPosition = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MemberCompany = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MemberEmAddress = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MemberEmCity = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    MemberEmProvince = table.Column<string>(type: "TEXT", maxLength: 45, nullable: true),
                    MemberEmPostalCode = table.Column<string>(type: "TEXT", maxLength: 7, nullable: false),
                    MemberEmPhoneNo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    MemberEmEmail = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    MemberIsMailingHome = table.Column<string>(type: "TEXT", nullable: false),
                    MemberIsNcGrad = table.Column<string>(type: "TEXT", nullable: false),
                    MemberEducation = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    MemberParticipation = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    MemberRegisteredAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MemberLastLogin = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MemberRenewalDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AccountID = table.Column<int>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberID);
                    table.ForeignKey(
                        name: "FK_Members_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PushEndpoint = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true),
                    PushP256DH = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true),
                    PushAuth = table.Column<string>(type: "TEXT", maxLength: 512, nullable: true),
                    VolunteerID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Accounts_VolunteerID",
                        column: x => x.VolunteerID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommitteeMeetings",
                columns: table => new
                {
                    CommitteeID = table.Column<int>(type: "INTEGER", nullable: false),
                    MeetingID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitteeMeetings", x => new { x.MeetingID, x.CommitteeID });
                    table.ForeignKey(
                        name: "FK_CommitteeMeetings_Committees_CommitteeID",
                        column: x => x.CommitteeID,
                        principalTable: "Committees",
                        principalColumn: "CommitteeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommitteeMeetings_Meetings_MeetingID",
                        column: x => x.MeetingID,
                        principalTable: "Meetings",
                        principalColumn: "MeetingID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActionItems",
                columns: table => new
                {
                    ActionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActionName = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    ActionDescription = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    ActionDueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ActionNotes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true),
                    MeetingID = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberID = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionItems", x => x.ActionID);
                    table.ForeignKey(
                        name: "FK_ActionItems_Meetings_MeetingID",
                        column: x => x.MeetingID,
                        principalTable: "Meetings",
                        principalColumn: "MeetingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActionItems_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    AttendanceID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AttendanceFirstRecent = table.Column<bool>(type: "INTEGER", nullable: false),
                    AttendanceSecondRecent = table.Column<bool>(type: "INTEGER", nullable: false),
                    AttendanceThirdRecent = table.Column<bool>(type: "INTEGER", nullable: false),
                    MemberID = table.Column<int>(type: "INTEGER", nullable: false),
                    MeetingID = table.Column<int>(type: "INTEGER", nullable: false),
                    AttendanceDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MemberSummaryVMID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.AttendanceID);
                    table.ForeignKey(
                        name: "FK_Attendances_Meetings_MeetingID",
                        column: x => x.MeetingID,
                        principalTable: "Meetings",
                        principalColumn: "MeetingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendances_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MemberCommitteeEnrolls",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "INTEGER", nullable: false),
                    CommitteeID = table.Column<int>(type: "INTEGER", nullable: false),
                    CommitteeRoleID = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberSummaryVMID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberCommitteeEnrolls", x => new { x.MemberID, x.CommitteeID, x.CommitteeRoleID });
                    table.ForeignKey(
                        name: "FK_MemberCommitteeEnrolls_CommitteeRoles_CommitteeRoleID",
                        column: x => x.CommitteeRoleID,
                        principalTable: "CommitteeRoles",
                        principalColumn: "CommitteeRoleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberCommitteeEnrolls_Committees_CommitteeID",
                        column: x => x.CommitteeID,
                        principalTable: "Committees",
                        principalColumn: "CommitteeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberCommitteeEnrolls_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UploadedFiles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    MimeType = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    MeetingID = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    MemberID = table.Column<int>(type: "INTEGER", nullable: true),
                    TaskID = table.Column<int>(type: "INTEGER", nullable: true),
                    ActionItemActionID = table.Column<int>(type: "INTEGER", nullable: true),
                    SupplimentaryDocument_Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedFiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UploadedFiles_ActionItems_ActionItemActionID",
                        column: x => x.ActionItemActionID,
                        principalTable: "ActionItems",
                        principalColumn: "ActionID");
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Meetings_MeetingID",
                        column: x => x.MeetingID,
                        principalTable: "Meetings",
                        principalColumn: "MeetingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Members_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Members",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileContent",
                columns: table => new
                {
                    FileContentID = table.Column<int>(type: "INTEGER", nullable: false),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileContent", x => x.FileContentID);
                    table.ForeignKey(
                        name: "FK_FileContent_UploadedFiles_FileContentID",
                        column: x => x.FileContentID,
                        principalTable: "UploadedFiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleID",
                table: "Accounts",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionItems_MeetingID",
                table: "ActionItems",
                column: "MeetingID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionItems_MemberID",
                table: "ActionItems",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_MeetingID",
                table: "Attendances",
                column: "MeetingID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_MemberID",
                table: "Attendances",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_MemberSummaryVMID",
                table: "Attendances",
                column: "MemberSummaryVMID");

            migrationBuilder.CreateIndex(
                name: "IX_CommitteeMeetings_CommitteeID",
                table: "CommitteeMeetings",
                column: "CommitteeID");

            migrationBuilder.CreateIndex(
                name: "IX_Committees_DivisionID",
                table: "Committees",
                column: "DivisionID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_CommitteeID",
                table: "Meetings",
                column: "CommitteeID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCommitteeEnrolls_CommitteeID",
                table: "MemberCommitteeEnrolls",
                column: "CommitteeID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCommitteeEnrolls_CommitteeRoleID",
                table: "MemberCommitteeEnrolls",
                column: "CommitteeRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCommitteeEnrolls_MemberSummaryVMID",
                table: "MemberCommitteeEnrolls",
                column: "MemberSummaryVMID");

            migrationBuilder.CreateIndex(
                name: "IX_Members_AccountID",
                table: "Members",
                column: "AccountID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_MemberEmail",
                table: "Members",
                column: "MemberEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_VolunteerID",
                table: "Subscriptions",
                column: "VolunteerID");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_ActionItemActionID",
                table: "UploadedFiles",
                column: "ActionItemActionID");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_MeetingID",
                table: "UploadedFiles",
                column: "MeetingID");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_MemberID",
                table: "UploadedFiles",
                column: "MemberID");

            ExtraMigrationStep.Steps(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "CommitteeMeetings");

            migrationBuilder.DropTable(
                name: "FileContent");

            migrationBuilder.DropTable(
                name: "MemberCommitteeEnrolls");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "UploadedFiles");

            migrationBuilder.DropTable(
                name: "CommitteeRoles");

            migrationBuilder.DropTable(
                name: "ActionItems");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Committees");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Divisions");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
