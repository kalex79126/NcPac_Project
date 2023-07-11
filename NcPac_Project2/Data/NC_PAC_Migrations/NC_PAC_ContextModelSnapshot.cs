﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NcPac_Project2.Data;

#nullable disable

namespace NcPac_Project2.Data.NC_PAC_Migrations
{
    [DbContext(typeof(NC_PAC_Context))]
    partial class NC_PAC_ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.15");

            modelBuilder.Entity("NcPac_Project2.Models.Account", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CompleteForm")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Salutaion")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("NcPac_Project2.Models.ActionItem", b =>
                {
                    b.Property<int>("ActionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ActionDescription")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ActionDueDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ActionName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("ActionNotes")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MeetingID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MemberID")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("ActionID");

                    b.HasIndex("MeetingID");

                    b.HasIndex("MemberID");

                    b.ToTable("ActionItems");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Attendance", b =>
                {
                    b.Property<int>("AttendanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AttendanceDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("AttendanceFirstRecent")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AttendanceSecondRecent")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AttendanceThirdRecent")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MeetingID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MemberID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MemberSummaryVMID")
                        .HasColumnType("INTEGER");

                    b.HasKey("AttendanceID");

                    b.HasIndex("MeetingID");

                    b.HasIndex("MemberID");

                    b.HasIndex("MemberSummaryVMID");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Auditing.FileContent", b =>
                {
                    b.Property<int>("FileContentID")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Content")
                        .HasColumnType("BLOB");

                    b.HasKey("FileContentID");

                    b.ToTable("FileContent");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Auditing.UploadedFile", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("MimeType")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("UploadedFiles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("UploadedFile");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Committee", b =>
                {
                    b.Property<int>("CommitteeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CommitteeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("DivisionID")
                        .HasColumnType("INTEGER");

                    b.HasKey("CommitteeID");

                    b.HasIndex("DivisionID");

                    b.ToTable("Committees");
                });

            modelBuilder.Entity("NcPac_Project2.Models.CommitteeMeeting", b =>
                {
                    b.Property<int>("MeetingID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CommitteeID")
                        .HasColumnType("INTEGER");

                    b.HasKey("MeetingID", "CommitteeID");

                    b.HasIndex("CommitteeID");

                    b.ToTable("CommitteeMeetings");
                });

            modelBuilder.Entity("NcPac_Project2.Models.CommitteeRole", b =>
                {
                    b.Property<int>("CommitteeRoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("RoleDescription")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.HasKey("CommitteeRoleID");

                    b.ToTable("CommitteeRoles");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Division", b =>
                {
                    b.Property<int>("DivisionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DivisionTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("DivisionID");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Meeting", b =>
                {
                    b.Property<int>("MeetingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CommitteeID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("MeetingDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MeetingNotes")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("MeetingTitle")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("MeetingID");

                    b.HasIndex("CommitteeID");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Member", b =>
                {
                    b.Property<int>("MemberID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MemberAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberCity")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberCompany")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberEducation")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberEmAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberEmCity")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberEmEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberEmPhoneNo")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberEmPostalCode")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberEmProvince")
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberEmail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberFirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberIsMailingHome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberIsNcGrad")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("MemberLastLogin")
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberLastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberParticipation")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberPhoneNo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberPosition")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberPostalCode")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("TEXT");

                    b.Property<string>("MemberProvince")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("MemberRegisteredAt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("MemberRenewalDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<string>("Salutation")
                        .HasMaxLength(6)
                        .HasColumnType("TEXT");

                    b.HasKey("MemberID");

                    b.HasIndex("AccountID")
                        .IsUnique();

                    b.HasIndex("MemberEmail")
                        .IsUnique();

                    b.ToTable("Members");
                });

            modelBuilder.Entity("NcPac_Project2.Models.MemberCommitteeEnroll", b =>
                {
                    b.Property<int>("MemberID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CommitteeID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CommitteeRoleID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MemberSummaryVMID")
                        .HasColumnType("INTEGER");

                    b.HasKey("MemberID", "CommitteeID", "CommitteeRoleID");

                    b.HasIndex("CommitteeID");

                    b.HasIndex("CommitteeRoleID");

                    b.HasIndex("MemberSummaryVMID");

                    b.ToTable("MemberCommitteeEnrolls");
                });

            modelBuilder.Entity("NcPac_Project2.Models.NotInDataModel.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PushAuth")
                        .HasMaxLength(512)
                        .HasColumnType("TEXT");

                    b.Property<string>("PushEndpoint")
                        .HasMaxLength(512)
                        .HasColumnType("TEXT");

                    b.Property<string>("PushP256DH")
                        .HasMaxLength(512)
                        .HasColumnType("TEXT");

                    b.Property<int>("VolunteerID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VolunteerID");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RoleCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleDescription")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RoleUpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("NcPac_Project2.ViewModels.MemberSummaryVM", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AppointedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("CommitteeName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Company")
                        .HasColumnType("TEXT");

                    b.Property<string>("DivisionName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("JobTitle")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastAttendanceDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasColumnType("TEXT");

                    b.Property<string>("NcGrad")
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberOfYearsPac")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PacChairs")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RegisteredAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RenewedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToView("MemberSummaries");
                });

            modelBuilder.Entity("NcPac_Project2.Models.MeetingDocument", b =>
                {
                    b.HasBaseType("NcPac_Project2.Models.Auditing.UploadedFile");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<int>("MeetingID")
                        .HasColumnType("INTEGER");

                    b.HasIndex("MeetingID");

                    b.HasDiscriminator().HasValue("MeetingDocument");
                });

            modelBuilder.Entity("NcPac_Project2.Models.MemberDocument", b =>
                {
                    b.HasBaseType("NcPac_Project2.Models.Auditing.UploadedFile");

                    b.Property<int>("MemberID")
                        .HasColumnType("INTEGER");

                    b.HasIndex("MemberID");

                    b.HasDiscriminator().HasValue("MemberDocument");
                });

            modelBuilder.Entity("NcPac_Project2.Models.NotInDataModel.SupplimentaryDocument", b =>
                {
                    b.HasBaseType("NcPac_Project2.Models.Auditing.UploadedFile");

                    b.Property<int?>("ActionItemActionID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT")
                        .HasColumnName("SupplimentaryDocument_Description");

                    b.Property<int>("TaskID")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ActionItemActionID");

                    b.HasDiscriminator().HasValue("SupplimentaryDocument");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Account", b =>
                {
                    b.HasOne("NcPac_Project2.Models.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("NcPac_Project2.Models.ActionItem", b =>
                {
                    b.HasOne("NcPac_Project2.Models.Meeting", "Meeting")
                        .WithMany("ActionItems")
                        .HasForeignKey("MeetingID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NcPac_Project2.Models.Member", "Member")
                        .WithMany("ActionItems")
                        .HasForeignKey("MemberID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Meeting");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Attendance", b =>
                {
                    b.HasOne("NcPac_Project2.Models.Meeting", "Meeting")
                        .WithMany("Attendances")
                        .HasForeignKey("MeetingID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NcPac_Project2.Models.Member", "Member")
                        .WithMany("Attendances")
                        .HasForeignKey("MemberID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NcPac_Project2.ViewModels.MemberSummaryVM", null)
                        .WithMany("Attendances")
                        .HasForeignKey("MemberSummaryVMID");

                    b.Navigation("Meeting");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Auditing.FileContent", b =>
                {
                    b.HasOne("NcPac_Project2.Models.Auditing.UploadedFile", "UploadedFile")
                        .WithOne("FileContent")
                        .HasForeignKey("NcPac_Project2.Models.Auditing.FileContent", "FileContentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UploadedFile");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Committee", b =>
                {
                    b.HasOne("NcPac_Project2.Models.Division", "Division")
                        .WithMany("Committees")
                        .HasForeignKey("DivisionID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Division");
                });

            modelBuilder.Entity("NcPac_Project2.Models.CommitteeMeeting", b =>
                {
                    b.HasOne("NcPac_Project2.Models.Committee", "Committee")
                        .WithMany("CommitteeMeetings")
                        .HasForeignKey("CommitteeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NcPac_Project2.Models.Meeting", "Meeting")
                        .WithMany("CommitteeMeetings")
                        .HasForeignKey("MeetingID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Committee");

                    b.Navigation("Meeting");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Meeting", b =>
                {
                    b.HasOne("NcPac_Project2.Models.Committee", "Committee")
                        .WithMany()
                        .HasForeignKey("CommitteeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Committee");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Member", b =>
                {
                    b.HasOne("NcPac_Project2.Models.Account", "Account")
                        .WithOne("Member")
                        .HasForeignKey("NcPac_Project2.Models.Member", "AccountID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("NcPac_Project2.Models.MemberCommitteeEnroll", b =>
                {
                    b.HasOne("NcPac_Project2.Models.Committee", "Committee")
                        .WithMany("MemberCommitteeEnrolls")
                        .HasForeignKey("CommitteeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NcPac_Project2.Models.CommitteeRole", "CommitteeRole")
                        .WithMany("MemberCommitteeEnrolls")
                        .HasForeignKey("CommitteeRoleID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NcPac_Project2.Models.Member", "Member")
                        .WithMany("MemberCommitteeEnrolls")
                        .HasForeignKey("MemberID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("NcPac_Project2.ViewModels.MemberSummaryVM", null)
                        .WithMany("MemberCommitteeEnrolls")
                        .HasForeignKey("MemberSummaryVMID");

                    b.Navigation("Committee");

                    b.Navigation("CommitteeRole");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("NcPac_Project2.Models.NotInDataModel.Subscription", b =>
                {
                    b.HasOne("NcPac_Project2.Models.Account", "Volunteer")
                        .WithMany("Subscriptions")
                        .HasForeignKey("VolunteerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Volunteer");
                });

            modelBuilder.Entity("NcPac_Project2.Models.MeetingDocument", b =>
                {
                    b.HasOne("NcPac_Project2.Models.Meeting", "Meeting")
                        .WithMany("MeetingMinutes")
                        .HasForeignKey("MeetingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meeting");
                });

            modelBuilder.Entity("NcPac_Project2.Models.MemberDocument", b =>
                {
                    b.HasOne("NcPac_Project2.Models.Member", "Member")
                        .WithMany("MemberDocuments")
                        .HasForeignKey("MemberID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("NcPac_Project2.Models.NotInDataModel.SupplimentaryDocument", b =>
                {
                    b.HasOne("NcPac_Project2.Models.ActionItem", "ActionItem")
                        .WithMany("SupplimentaryDocuments")
                        .HasForeignKey("ActionItemActionID");

                    b.Navigation("ActionItem");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Account", b =>
                {
                    b.Navigation("Member");

                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("NcPac_Project2.Models.ActionItem", b =>
                {
                    b.Navigation("SupplimentaryDocuments");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Auditing.UploadedFile", b =>
                {
                    b.Navigation("FileContent");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Committee", b =>
                {
                    b.Navigation("CommitteeMeetings");

                    b.Navigation("MemberCommitteeEnrolls");
                });

            modelBuilder.Entity("NcPac_Project2.Models.CommitteeRole", b =>
                {
                    b.Navigation("MemberCommitteeEnrolls");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Division", b =>
                {
                    b.Navigation("Committees");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Meeting", b =>
                {
                    b.Navigation("ActionItems");

                    b.Navigation("Attendances");

                    b.Navigation("CommitteeMeetings");

                    b.Navigation("MeetingMinutes");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Member", b =>
                {
                    b.Navigation("ActionItems");

                    b.Navigation("Attendances");

                    b.Navigation("MemberCommitteeEnrolls");

                    b.Navigation("MemberDocuments");
                });

            modelBuilder.Entity("NcPac_Project2.Models.Role", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("NcPac_Project2.ViewModels.MemberSummaryVM", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("MemberCommitteeEnrolls");
                });
#pragma warning restore 612, 618
        }
    }
}
