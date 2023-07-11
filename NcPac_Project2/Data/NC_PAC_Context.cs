using NcPac_Project2.Models;
using Microsoft.EntityFrameworkCore;
using NcPac_Project2.ViewModels;
using NcPac_Project2.Models.NotInDataModel;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using NcPac_Project2.Models.Auditing;

namespace NcPac_Project2.Data
{
    public class NC_PAC_Context : DbContext
    {
        public NC_PAC_Context(DbContextOptions<NC_PAC_Context> options)
    : base(options)
        {
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<ActionItem> ActionItems { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<MemberCommitteeEnroll> MemberCommitteeEnrolls { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<MemberDocument> MemberDocuments { get; set; }
        public DbSet<SupplimentaryDocument> SupplimentaryDocuments { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<MemberSummaryVM> MemberSummaries { get; set; }
        public DbSet<MeetingDocument> MeetingDocuments{ get; set; }
        public DbSet<CommitteeMeeting> CommitteeMeetings { get; set; }
        public DbSet<CommitteeRole> CommitteeRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //For the View - if TheKey is the name of the property to use as a primary key
            modelBuilder
                .Entity<MemberSummaryVM>()
                .ToView(nameof(MemberSummaries))
                .HasKey(v => v.ID);


            //Unique index to the Account Email
            modelBuilder.Entity<Account>()
                .HasIndex(v => new { v.Email })
                .IsUnique();

            //Add a unique index to the Member personal Email
            modelBuilder.Entity<Member>()
            .HasIndex(m => new { m.MemberEmail })
            .IsUnique();

            //one to many between Committees to Division
            modelBuilder.Entity<Committee>()
                .HasOne<Division>(d => d.Division)
                .WithMany(p => p.Committees)
                .HasForeignKey(p => p.DivisionID)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.HasDefaultSchema("NcPac");
            //many to many intersection
            modelBuilder.Entity<CommitteeMeeting>()
                .HasKey(t => new { t.MeetingID, t.CommitteeID });

            //Note: Allow Cascade Delete from Committee to CommitteeMeeting
            modelBuilder.Entity<CommitteeMeeting>()
                .HasOne<Committee>(e => e.Committee)
                .WithMany(m => m.CommitteeMeetings)
                .HasForeignKey(e => e.CommitteeID)
                .OnDelete(DeleteBehavior.Restrict);

            //Note: Allow Cascade Delete from Meeting to CommitteeMeeting
            modelBuilder.Entity<CommitteeMeeting>()
                .HasOne<Meeting>(e => e.Meeting)
                .WithMany(m => m.CommitteeMeetings)
                .HasForeignKey(e => e.MeetingID)
                .OnDelete(DeleteBehavior.Restrict);

            //one to many between Meeting to Attendances
            modelBuilder.Entity<Attendance>()
                .HasOne<Meeting>(d => d.Meeting)
                .WithMany(p => p.Attendances)
                .HasForeignKey(p => p.MeetingID)
                .OnDelete(DeleteBehavior.Restrict);

            //one to many between Meeting to ActionItems
            modelBuilder.Entity<ActionItem>()
                .HasOne<Meeting>(d => d.Meeting)
                .WithMany(p => p.ActionItems)
                .HasForeignKey(p => p.MeetingID)
                .OnDelete(DeleteBehavior.Restrict);

            //many to many intersection
            modelBuilder.Entity<MemberCommitteeEnroll>()
                .HasKey(t => new { t.MemberID, t.CommitteeID, t.CommitteeRoleID });

            //many to many
            //Note: Allow Cascade Delete from Member to Enroll
            modelBuilder.Entity<MemberCommitteeEnroll>()
                .HasOne<Member>(e => e.Member)
                .WithMany(m => m.MemberCommitteeEnrolls)
                .HasForeignKey(e => e.MemberID)
                .OnDelete(DeleteBehavior.Restrict);

            //one to many between Committee to Enrolls
            modelBuilder.Entity<MemberCommitteeEnroll>()
                .HasOne<Committee>(d => d.Committee)
                .WithMany(p => p.MemberCommitteeEnrolls)
                .HasForeignKey(p => p.CommitteeID)
                .OnDelete(DeleteBehavior.Restrict);

            //one to many between Committee to Enrolls
            modelBuilder.Entity<MemberCommitteeEnroll>()
                .HasOne<CommitteeRole>(d => d.CommitteeRole)
                .WithMany(p => p.MemberCommitteeEnrolls)
                .HasForeignKey(p => p.CommitteeRoleID)
                .OnDelete(DeleteBehavior.Restrict);

            //one to many between Member to Attendances
            modelBuilder.Entity<Attendance>()
                .HasOne<Member>(d => d.Member)
                .WithMany(p => p.Attendances)
                .HasForeignKey(p => p.MemberID)
                .OnDelete(DeleteBehavior.Restrict);

            //one to many between Member to ActionItem
            modelBuilder.Entity<ActionItem>()
                .HasOne<Member>(d => d.Member)
                .WithMany(p => p.ActionItems)
                .HasForeignKey(p => p.MemberID)
                .OnDelete(DeleteBehavior.Restrict);

            //one to many between Role to Account
            modelBuilder.Entity<Account>()
                .HasOne<Role>(d => d.Role)
                .WithMany(p => p.Accounts)
                .HasForeignKey(p => p.RoleID)
                .OnDelete(DeleteBehavior.Restrict);

            //one-to-one between Account and Member
            modelBuilder.Entity<Account>()
                .HasOne<Member>(p => p.Member)
                .WithOne(a => a.Account)
                .HasForeignKey<Member>(a => a.AccountID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
