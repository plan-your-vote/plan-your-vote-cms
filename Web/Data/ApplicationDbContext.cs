using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VotingModelLibrary.Models;
using VotingModelLibrary.Models.Theme;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Image>().HasKey(i => new { i.ThemeName, i.ID });
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
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
