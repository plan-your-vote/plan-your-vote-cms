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

            DbSet<BallotIssue> BallotIssues { get; set; }
            DbSet<CandiateRace> CandiateRaces { get; set; }
            DbSet<Candidate> Candidates { get; set; }
            DbSet<Contact> Contacts { get; set; }
            DbSet<IssueOption> IssueOptions { get; set; }
            DbSet<Organization> Organizations { get; set; }
            DbSet<Race> Races { get; set; }
        }
}
