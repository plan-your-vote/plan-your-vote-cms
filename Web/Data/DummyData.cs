using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

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

                var elections = GetElections().ToArray();
                context.Elections.AddRange(elections);
                context.SaveChanges();

                var pollingStations = GetPollingStations().ToArray();
                context.PollingStations.AddRange(pollingStations);
                context.SaveChanges();

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
                UserName = "a@a.a",
                SecurityStamp = Guid.NewGuid().ToString()
            };


            var result = await userManager.CreateAsync(user, "P@$$w0rd");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }

            var user1 = new IdentityUser
            {
                Email = "m@m.m",
                UserName = "m@m.m",
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
                    Name = "The United People for Change",
                    Description = "_DESCRIPTION_",
                },
                new Organization()
                {
                    Name = "The Second Organization",
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
                    ElectionId = 1
                },
                new Race
                {
                    PositionName = "Councillor",
                    NumberNeeded = 10,
                    ElectionId = 1
                },
                new Race
                {
                    PositionName = "Park Commisioner",
                    NumberNeeded = 7,
                    ElectionId = 1
                },
                new Race
                {
                    PositionName = "School Trustee",
                    NumberNeeded = 9,
                    ElectionId = 1
                }
            };
        }

        private static List<Candidate> GetCandidates()
        {
            return new List<Candidate>
            {
                new Candidate
                {
                    Name = "LAMARCHE Jason",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor0.jpg",
                    OrganizationId = 1,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "HANSEN Mike",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor1.jpg",
                    OrganizationId = 2,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "YOUNG Wai",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor12.jpg",
                    OrganizationId = 3,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "PACHECO Tony",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor9.jpg",
                    OrganizationId = 2,
                    ElectionId = 1
                }
                ,
                new Candidate
                {
                    Name = "DI IORIO Danny",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor9.jpg",
                    OrganizationId = 1,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "STEWART Kennedy",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor7.jpg",
                    OrganizationId = 2,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "YANO John",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor2.jpg",
                    OrganizationId = 1,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "CHEN David",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor3.jpg",
                    OrganizationId = 2,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "SYLVESTER Shauna",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor4.jpg",
                    OrganizationId = 2,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "CASSIDY Sean",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor8.jpg",
                    OrganizationId = 1,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "BREMNER Hector",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor10.jpg",
                    OrganizationId = 1,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "FOGAL Connie",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor11.jpg",
                    OrganizationId = 1,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "BASRA Nycki",
                    Picture = "https://vancouver.ca/plan-your-vote/img/councillor2.jpg",
                    OrganizationId = 2,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "RAUNET Françoise",
                    Picture = "https://vancouver.ca/plan-your-vote/img/councillor9.jpg",
                    OrganizationId = 2,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "ROBERTS Anne",
                    Picture = "https://vancouver.ca/plan-your-vote/img/councillor15.jpg",
                    OrganizationId = 2,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "BLIGH Rebecca",
                    Picture = "https://vancouver.ca/plan-your-vote/img/councillor22.jpg",
                    OrganizationId = 2,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "GREWAL David",
                    Picture = "https://vancouver.ca/plan-your-vote/img/councillor40.jpg",
                    OrganizationId = 2,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "LI Morning",
                    Picture = "https://vancouver.ca/plan-your-vote/img/councillor59.jpg",
                    OrganizationId = 2,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "ZARUDINA Olga",
                    Picture = "https://vancouver.ca/plan-your-vote/img/park0.jpg",
                    OrganizationId = 3,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "NEMETZ Steven L",
                    Picture = "https://vancouver.ca/plan-your-vote/img/park5.jpg",
                    OrganizationId = 3,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "COUPAR John",
                    Picture = "https://vancouver.ca/plan-your-vote/img/park10.jpg",
                    OrganizationId = 3,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "MCGARRIGLE Kathy",
                    Picture = "https://vancouver.ca/plan-your-vote/img/park11.jpg",
                    OrganizationId = 3,
                    ElectionId = 1
                },
                new Candidate
                {
                    Name = "MACKINNON Stuart",
                    Picture = "https://vancouver.ca/plan-your-vote/img/park31.jpg",
                    OrganizationId = 3,
                    ElectionId = 1
                },


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
                },
                new Contact()
                {
                    CandidateId = 4,
                    ContactMethod = "Tel",
                    ContactValue = "222-222-5252"
                },
                new Contact()
                {
                    CandidateId = 5,
                    ContactMethod = "Email",
                    ContactValue = "dd@dd.dd"
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
                },
                new CandidateRace()
                {
                    RaceId = 1,
                    CandidateId = 4,
                    PositionName = "Mayor",
                    PlatformInfo = @"If elected, Tony promises change for everyone. And not just small change, but like, Loonies and Toonies and stuff."
                },
                new CandidateRace()
                {
                    RaceId = 1,
                    CandidateId = 5,
                    PositionName = "Mayor",
                    PlatformInfo = @"If elected, Danny will have made sure that Tony could not consolidate power for himself."
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
                    ElectionId = 1
                },
                new BallotIssue()
                {
                    BallotIssueTitle = "CAPITAL MAINTENANCE AND RENOVATION PROGRAMS FOR EXISTING COMMUNITY FACILITIES, CIVIC FACILITIES, AND PARKS",
                    Description = @"This question seeks authority to borrow funds to be used in carrying out the basic capital works program with respect to capital maintenance and renovation programs for existing community facilities, civic facilities, and parks.

Are you in favour of Council having the authority, without further assent of the electors, to pass bylaws between January 1, 2019, and December 31, 2022, to borrow an aggregate $99,557,000 for the following purposes?",
                    ElectionId = 1
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

        private static List<Election> GetElections()
        {
            return new List<Election>()
            {
                new Election()
                {
                    ElectionName = "City of Vancouver 2018 Municiple Election",
                    EndDate = "October 21 2018",
                    StartDate = "October 21 2018",
                    Description = "Voting in an election is one of the most important things a citizen can do in their community and country."
                },
                new Election()
                {
                    ElectionName = "Canadian Federal Election, 2019",
                    EndDate = "October 21 2019",
                    StartDate = "October 21 2019",
                    Description = "The 2019 Canadian federal election is scheduled to take place on or before October 21, 2019. The October 21 date of the vote is determined by the fixed-date procedures in the Canada Elections Act"
                },
                new Election()
                {
                    ElectionName = "City of Vancouver 2020 Municiple Election",
                    EndDate = "October 21 2020",
                    StartDate = "October 21 2020",
                    Description = "Voting in an election is one of the most important things a citizen can do in their community and country."
                }
            };
        }

        private static List<PollingStation> GetPollingStations()
        {
            return new List<PollingStation>()
            {
                new PollingStation()
                {
                   
                    ElectionId = 1,
                    Name = "Holy Trinity Anglican Church",
                    AdditionalInfo = "",
                    Latitude = 49.27376,
                    Longitute = -123.127989,
                    Address = "1440 W 12th Avenue",
                    WheelchairInfo = "main entrance on West 12th Ave",
                    ParkingInfo = "street and church parking lot",
                    WashroomInfo = "",
                    GeneralAccessInfo = ""
                },
                new PollingStation()
                {
                    
                    ElectionId = 1,
                    Name = "Grace Vancouver Church",
                    AdditionalInfo = "",
                    Latitude = 48.888,
                    Longitute = -121.00,
                    Address = "1696 W 7th Avenue",
                    WheelchairInfo = "via ramp at the side entrance to the sanctuary, on West 7th Ave",
                    ParkingInfo = "street",
                    WashroomInfo = "",
                    GeneralAccessInfo = ""
                },
                new PollingStation()
                {
                   
                    ElectionId = 2,
                    Name = "Lord Tennyson Elementary School",
                    AdditionalInfo = "",
                    Latitude = 49.4444,
                    Longitute = -122.555,
                    Address = "1936 W 10th Avenue",
                    WheelchairInfo = "south side entrance on West 11th Ave",
                    ParkingInfo = "street",
                    WashroomInfo = "wheelchair accessible washrooms are on the 3rd floor via chair lift",
                    GeneralAccessInfo = ""
                },
                new PollingStation()
                {
                  
                    ElectionId = 2,
                    Name = "Gathering Place Community Centre",
                    AdditionalInfo = "Handicap parking unavailable",
                    Latitude = 50.110,
                    Longitute = -123.444,
                    Address = "609 Helmcken St",
                    WheelchairInfo = "main entrance at the corner of Helmcken St and Seymour St",
                    ParkingInfo = "street",
                    WashroomInfo = "",
                    GeneralAccessInfo = ""
                },
                new PollingStation()
                {
                 
                    ElectionId = 3,
                    Name = "False Creek Community Centre",
                    AdditionalInfo = "",
                    Latitude = 49.5677,
                    Longitute = -121.127989,
                    Address = "1318 Cartwright St",
                    WheelchairInfo = "main entrance on Cartwright St",
                    ParkingInfo = "street and community centre parking",
                    WashroomInfo = "",
                    GeneralAccessInfo = ""
                },
                new PollingStation()
                {
                   
                    ElectionId = 3,
                    Name = "Roundhouse Community Arts Centre",
                    AdditionalInfo = "",
                    Latitude = 49.6999,
                    Longitute = -124.127989,
                    Address = "181 Roundhouse Mews",
                    WheelchairInfo = "main entrance on Roundhouse Mews",
                    ParkingInfo = "street parking, underground parking off Drake St",
                    WashroomInfo = "",
                    GeneralAccessInfo = ""
                }
            };
        }
    }
}