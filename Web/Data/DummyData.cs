using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingModelLibrary.Models;

namespace Web.Data
{
    public static class DummyData
    {
        public static async Task Initialize(IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                UserManager<IdentityUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();
                RoleManager<IdentityRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();


                context.Database.EnsureCreated();

                if (context.Candidates.Any()) { return; }

                await InsertUserAsync(userManager, roleManager);

                var organizations = GetOrganizations().ToArray();
                context.Organizations.AddRange(organizations);
                context.SaveChanges();

                var races = GetRaces().ToArray();
                context.Races.AddRange(races);
                context.SaveChanges();

                var candidates = GetCandidates().ToArray();
                context.Candidates.AddRange(candidates);
                context.SaveChanges();

                var contacts = GetContacts().ToArray();
                context.Contacts.AddRange(contacts);
                context.SaveChanges();

                var candidateRaces = GetCandidateRaces().ToArray();
                context.CandidateRaces.AddRange(candidateRaces);
                context.SaveChanges();

                var ballotIssues = GetBallotIssues().ToArray();
                context.BallotIssues.AddRange(ballotIssues);
                context.SaveChanges();

                var issueOptions = GetIssueOptions().ToArray();
                context.IssueOptions.AddRange(issueOptions);
                context.SaveChanges();
            }
        }

        public static async Task InsertUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            var role1 = new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "Admin"
            };

            var role2 = new IdentityRole
            {
                Name = "Member",
                NormalizedName = "Member"
            };

            if (await roleManager.FindByNameAsync(role1.Name) == null)
            {
                
                await roleManager.CreateAsync(role1);
            }
            if (await roleManager.FindByNameAsync(role2.Name) == null)
            {
                await roleManager.CreateAsync(role2);
            }

            var user = new IdentityUser
            {
                Email = "a@a.a",
                UserName = "a",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // var result = await userManager.CreateAsync(user);

            var result = await userManager.CreateAsync(user, "P@$$w0rd");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }

            var user1 = new IdentityUser
            {
                Email = "m@m.m",
                UserName = "m",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // var result = await userManager.CreateAsync(user);

            var result1 = await userManager.CreateAsync(user1, "P@$$w0rd");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user1, "Member");
            }
        }

        private static List<Organization> GetOrganizations()
        {
            return new List<Organization>()
            {
                new Organization()
                {
                    Name = "_ORGANIZATION_JASON_LAMARCHE_",
                    Description = "_DESCRIPTION_",
                },
                new Organization()
                {
                    Name = "_ORGANIZATION_MIKE_HANSEN_",
                    Description = "_DESCRIPTION_",
                },
                new Organization()
                {
                    Name = "Coalition Vancouver",
                    Description = "Coalition is 100% for the people!",
                }
            };
        }

        private static List<Race> GetRaces()
        {
            return new List<Race>
            {
                new Race
                {
                    PositionName = "Mayor",
                    NumberNeeded = 1,
                },
                new Race
                {
                    PositionName = "Councillor",
                    NumberNeeded = 10,
                },
                new Race
                {
                    PositionName = "Park Commisioner",
                    NumberNeeded = 7,
                },
                new Race
                {
                    PositionName = "School Trustee",
                    NumberNeeded = 9,
                }
            };
        }

        private static List<Candidate> GetCandidates()
        {
            return new List<Candidate>
            {
                new Candidate
                {
                    FirstName = "Jason",
                    LastName= "Lamarche",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor0.jpg",
                    Biography = @"Jason Lamarche loves Vancouver and has lived here for 21 years. Jason was born in Ottawa, Ontario.

Jason attended Langara and UBC with eight years experience at TD Bank, HSBC, CIBC. Jason was an executive for Canada's largest retail Cannabis distributor.

Jason has ten years experience in Municipal, Provincial, Federal politics; earning 37,286 votes in 2011 Vancouver Council election.",
                    OrganizationId = 1,
                },
                new Candidate
                {
                    FirstName = "Mike",
                    LastName = "Hansen",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor1.jpg",
                    Biography = @"Been in housing construction most of my life, truck driver, stock promoter and various other business. Founded the Canadian Hemp Growers Assoc. in 1996. 2005 on record at Senate Committee on national defence saying, ""terrorists"" are smuggling guns/drugs into Canada. 2005 Harm Reduction Pilot Project/monopolizing heroin to save lives, Men On Down Town East Society and Two Ravens Opioid Project.",
                    OrganizationId = 2,
                },
                new Candidate
                {
                    FirstName = "Wai",
                    LastName = "Young",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor12.jpg",
                    Biography = @"Wai has lived in Vancouver for over 50 years and is a community advocate, business owner, and past Member of Parliament with over three decades of civic and policy leadership. She is a birth mother of twins and a foster parent to seven children. She holds a degree in Sociology with post graduate work in Urban Planning and Mass Communications.",
                    OrganizationId = 3
                }
            };
        }

        private static List<Contact> GetContacts()
        {
            return new List<Contact>
            {
                new Contact()
                {
                    CandidateId = 1,
                    ContactMethod = "Tel",
                },
                new Contact()
                {
                    CandidateId = 1,
                    ContactMethod = "Email",
                    ContactValue = "MAYORLAMARCHE@protonmail.com",
                },
                new Contact()
                {
                    CandidateId = 2,
                    ContactMethod = "Tel",
                    ContactValue = "604 700 1652",
                },
                new Contact()
                {
                    CandidateId = 2,
                    ContactMethod = "Email",
                    ContactValue = "mikec.hansen@gmail.com",
                },
                new Contact()
                {
                    CandidateId = 3,
                    ContactMethod = "Tel",
                    ContactValue = "555-555-5555"
                },
                new Contact()
                {
                    CandidateId = 3,
                    ContactMethod = "Email",
                    ContactValue = "wai@young.ca"
                }
            };
        }

        private static List<CandidateRace> GetCandidateRaces()
        {
            return new List<CandidateRace>()
            {
                new CandidateRace()
                {
                    RaceId = 1,
                    CandidateId = 1,
                    PositionName = "Mayor",
                    PlatformInfo = @"If Jason Lamarche is elected Mayor of Vancouver he will use his Democratic support and Executive Powers to impose strict Rent Control with select 1 bedroom rents capped at $500 per month.

Jason will launch new Illegal Immigration Control Enforcement teams managed by Vancouver Police Department.

Jason will help Canadian citizens start, run, and expand small businesses and other development projects.

Jason is a true independent and will not accept donations from voters or businesses.",
                    TopIssues = @"RENT CONTROL 1 BR $500
ILLEGAL MIGRANT CONTROL
//PRO SMALL CANADA BUSINESS",
                },
                new CandidateRace()
                {
                    RaceId = 1,
                    CandidateId = 3, 
                    PositionName = "Mayor",
                    PlatformInfo = @"Wai Young and Coalition Vancouver are 100% for the People. With over 35 years of policy experience working with community, governments & business she is ready to lead. She is proven, experienced, and inclusive. As a former MP who brought many important infrastructure projects to the city, she understands the comprehensive issues facing Vancouver. She will bring back mutual respect on the streets and clean up our city."
                }
            };
        }

        private static List<BallotIssue> GetBallotIssues()
        {
            return new List<BallotIssue>()
            {
                new BallotIssue()
                {
                    BallotIssueTitle = "TRANSPORTATION AND TECHNOLOGY",
                    Description = @"This question seeks authority to borrow funds to be used in carrying out the basic capital works program with respect to transportation and technology.

Are you in favour of Council having the authority, without further assent of the electors, to pass bylaws between January 1, 2019, and December 31, 2022, to borrow an aggregate $100,353,000 for the following purposes?",
                },
                new BallotIssue()
                {
                    BallotIssueTitle = "CAPITAL MAINTENANCE AND RENOVATION PROGRAMS FOR EXISTING COMMUNITY FACILITIES, CIVIC FACILITIES, AND PARKS",
                    Description = @"This question seeks authority to borrow funds to be used in carrying out the basic capital works program with respect to capital maintenance and renovation programs for existing community facilities, civic facilities, and parks.

Are you in favour of Council having the authority, without further assent of the electors, to pass bylaws between January 1, 2019, and December 31, 2022, to borrow an aggregate $99,557,000 for the following purposes?",
                },
            };
        }

        private static List<IssueOption> GetIssueOptions()
        {
            return new List<IssueOption>()
            {
                new IssueOption()
                {
                    BallotIssueId = 1,
                    IssueOptionTitle = "How you plan to answer Question 1. Transportation and technology",
                    IssueOptionInfo = "Yes",
                },
                new IssueOption()
                {
                    BallotIssueId = 1,
                    IssueOptionTitle = "How you plan to answer Question 1. Transportation and technology",
                    IssueOptionInfo = "No",
                },
                new IssueOption()
                {
                    BallotIssueId = 2,
                    IssueOptionTitle = "How you plan to answer Question 2. Capital maintenance and renovation programs for existing community facilities, civic facilities, and parks",
                    IssueOptionInfo = "Yes",
                },
                new IssueOption()
                {
                    BallotIssueId = 2,
                    IssueOptionTitle = "How you plan to answer Question 2. Capital maintenance and renovation programs for existing community facilities, civic facilities, and parks",
                    IssueOptionInfo = "No",
                },
            };
        }
    }
}
