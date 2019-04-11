﻿using LPAuditService.Models.Account;
using LPAuditService.Models.Areas;
using LPAuditService.Models.Auditing;
using LPAuditService.Models.Calendar;
using LPAuditService.Models.Checking;
using System.Data.Entity;

namespace LPAuditService.Models
{
    public class LayoutProcessContext : DbContext
    {
        public LayoutProcessContext()
            :base("DefaultConnection")
        {
            
        }

        public DbSet<Role> LpaRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerLog> AnswerLogs { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<ChecklistLog> ChecklistLogs { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionLog> QuestionsLog { get; set; }
        public DbSet<UsersAudits> UsersAudits { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Area> Areas { get; set; }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<Part> Parts { get; set; }

        public DbSet<Audit> Audits { get; set; }
        public DbSet<AuditConfig> AuditConfigs { get; set; }
        public DbSet<AuditsChecklists> AuditsChecklists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasRequired(X => X.User);

            modelBuilder.Entity<Group>()
                .Property(e => e.chr_Key)
                .IsFixedLength();

            modelBuilder.Entity<Group>()
                .Property(e => e.chr_Description)
                .IsFixedLength();

            modelBuilder.Entity<Group>()
                .Property(e => e.chr_Icon)
                .IsFixedLength();

            modelBuilder.Entity<Group>()
                .Property(e => e.chr_Color)
                .IsFixedLength();

            modelBuilder.Entity<Group>()
                .Property(e => e.chr_Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Group>()
                .Property(e => e.chr_Reference)
                .IsUnicode(false);

            modelBuilder.Entity<Machine>()
                .Property(e => e.chr_Key)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Machine>()
                .Property(e => e.chr_Description)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Machine>()
                .Property(e => e.chr_Icon)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Machine>()
                .Property(e => e.chr_Color)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Machine>()
                .Property(e => e.chr_Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Machine>()
                .Property(e => e.chr_Watcher)
                .IsUnicode(false);

            modelBuilder.Entity<Machine>()
                .Property(e => e.chr_Supervisors)
                .IsUnicode(false);

            modelBuilder.Entity<Machine>()
                .Property(e => e.chr_TypeOper)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Machine>()
                .Property(e => e.chr_Reference)
                .IsUnicode(false);

            modelBuilder.Entity<Part>()
                .Property(e => e.chr_Key)
                .IsFixedLength();

            modelBuilder.Entity<Part>()
                .Property(e => e.chr_Description)
                .IsFixedLength();

            modelBuilder.Entity<Part>()
                .Property(e => e.chr_Icon)
                .IsFixedLength();

            modelBuilder.Entity<Part>()
                .Property(e => e.chr_Color)
                .IsFixedLength();

            modelBuilder.Entity<Part>()
                .Property(e => e.chr_Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Part>()
                .Property(e => e.chr_Reference)
                .IsUnicode(false);
        }
    }
}