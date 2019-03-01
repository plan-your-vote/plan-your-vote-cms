using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelLibrary.Models;

namespace Web.Data
{
        public class ApplicationDbContext : IdentityDbContext
        {
            public ApplicationDbContext(DbContextOptions options) : base(options)
            {

            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                //optionsBuilder.UseSqlite("Data Source=PlanYourVote.db");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<Contact>().HasKey(c => new { c.CandidateId, c.Type });
                modelBuilder.Entity<IssueOption>().HasKey(io => new { io.IssueTitle, io.OptionTitle });
            }

            public DbSet<BallotIssue> BallotIssues { get; set; }
            public DbSet<CandiateRace> CandiateRaces { get; set; }
            public DbSet<Candidate> Candidates { get; set; }
            public DbSet<Contact> Contacts { get; set; }
            public DbSet<IssueOption> IssueOptions { get; set; }
            public DbSet<Organization> Organizations { get; set; }
            public DbSet<Race> Races { get; set; }
        }
}
