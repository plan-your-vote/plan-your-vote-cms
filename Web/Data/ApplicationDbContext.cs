using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlanYourVoteLibrary2;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CandidateDetail>()
                .HasOne(cd => cd.Candidate)
                .WithMany(c => c.Details);
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
        public DbSet<PollingPlace> PollingPlaces { get; set; }
        public DbSet<PollingPlaceDate> PollingPlaceDates { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<CandidateDetail> CandidateDetails { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<OpenGraph> OpenGraph { get; set; }
    }
}
