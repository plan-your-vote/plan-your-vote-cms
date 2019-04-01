using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VotingModelLibrary.Models;
using Web.Models;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<State> StateSingleton { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<CandidateRace> CandidateRaces { get; set; }
        public DbSet<BallotIssue> BallotIssues { get; set; }
        public DbSet<IssueOption> IssueOptions { get; set; }
        public DbSet<PollingStation> PollingStations { get; set; }
        public DbSet<Theme> Theme { get; set; }
    }
}
