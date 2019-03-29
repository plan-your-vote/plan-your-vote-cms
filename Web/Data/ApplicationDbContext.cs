using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VotingModelLibrary.Models;
using Web.ViewModels;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<CandidateRace> CandidateRaces { get; set; }
        public DbSet<BallotIssue> BallotIssues { get; set; }
        public DbSet<IssueOption> IssueOptions { get; set; }
    }
}
