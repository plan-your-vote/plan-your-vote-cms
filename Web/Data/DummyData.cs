using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Data
{
    public static class DummyData
    {
        public static ApplicationDbContext _context;

        public const int DummyElectionId = 1; // Hardcoded

        public static async Task Initialize(ApplicationDbContext context, IApplicationBuilder app)
        {
            _context = context;

            context.Database.EnsureCreated();

            if (!context.Candidates.Any())
            {
                InitializeDatabase(context);
            }
            else
            {
                return;
            }

            await InsertUserAsync(app);
        }

        public static void InitializeDatabase(ApplicationDbContext context)
        {
            const string candidatesFile = "wwwroot/Data/candidates.json";
            List<JSONCandidate> candidateData = GetJsonData<JSONCandidate>(candidatesFile);



            var elections = GetElections().ToArray();
            context.Elections.AddRange(elections);
            context.SaveChanges();

            const string pollingStationsFile = "wwwroot/Data/pollingStations.json";
            List<JSONPollingStation> pollingStationsData = GetJsonData<JSONPollingStation>(pollingStationsFile);

            List<PollingStation> pollingStations = pollingStationsData
                .Select(psd => new PollingStation()
                {
                    ElectionId = DummyElectionId,
                    PollingStationId = psd.VotingPlaceID,
                    Name = psd.FacilityName,
                    Address = psd.FacilityAddress,
                    AdditionalInfo = psd.Location,
                    Latitude = psd.Latitude,
                    Longitude = psd.Longitude,
                    // = psd.AdvancedOnly,
                    // = psd.LocalArea,

                })
                .ToList();
            context.PollingStations.AddRange(pollingStations);
            context.SaveChanges();

            var organizations = GetOrganizations(candidateData).ToArray();
            context.Organizations.AddRange(organizations);
            context.SaveChanges();

            var races = GetRaces(candidateData).ToArray();
            context.Races.AddRange(races);
            context.SaveChanges();

            GetCandidatesAndContacts(candidateData);

            var candidateRaces = GetCandidateRaces(candidateData).ToArray();
            context.CandidateRaces.AddRange(candidateRaces);
            context.SaveChanges();

            var ballotIssues = GetBallotIssues().ToArray();
            context.BallotIssues.AddRange(ballotIssues);
            context.SaveChanges();

            var issueOptions = GetIssueOptions().ToArray();
            context.IssueOptions.AddRange(issueOptions);
            context.SaveChanges();
        }

        private static List<CandidateRace> GetCandidateRaces(List<JSONCandidate> candidateData)
        {
            List<CandidateRace> candidateRaces = new List<CandidateRace>();

            foreach (var existingCandidate in candidateData)
            {
                CandidateRace candidateRace = new CandidateRace()
                {
                    PlatformInfo = existingCandidate.Platform,
                    PositionName = existingCandidate.Position,
                };

                candidateRace.CandidateId = _context.Candidates
                    .Where(candidate => candidate.Name == existingCandidate.Name)
                    .First() // BUG in the future?
                    .CandidateId;

                candidateRace.RaceId = _context.Races
                    .Where(races => races.PositionName == existingCandidate.Position)
                    .First() //Potential bug
                    .RaceId;

                candidateRaces.Add(candidateRace);
            }

            return candidateRaces;
        }

        private static void GetCandidatesAndContacts(List<JSONCandidate> candidateData)
        {
            List<Contact> contacts = new List<Contact>();
            List<CandidateDetail> details = new List<CandidateDetail>();

            foreach (var existingCandidate in candidateData)
            {
                Candidate candidate = new Candidate()
                {
                    ElectionId = DummyElectionId,
                    Name = existingCandidate.Name,
                    Picture = "images/" + existingCandidate.Picture,
                    OrganizationId = _context.Organizations
                        .Where(organization => organization.Name == existingCandidate.Party)
                        .Single()
                        .OrganizationId,
                };

                _context.Candidates.Add(candidate);
                _context.SaveChanges();

                if (!string.IsNullOrEmpty(existingCandidate.Priority1))
                {
                    details.Add(new CandidateDetail()
                    {
                        Title = "Priority 1",
                        Format = CandidateDetailFormat.OrderedList,
                        Text = existingCandidate.Priority1,
                        Lang = Language.en,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Priority2))
                {

                    details.Add(new CandidateDetail()
                    {
                        Title = "Priority 2",
                        Format = CandidateDetailFormat.OrderedList,
                        Text = existingCandidate.Priority2,
                        Lang = Language.en,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Priority3))
                {
                    details.Add(new CandidateDetail()
                    {
                        Title = "Priority 3",
                        Format = CandidateDetailFormat.OrderedList,
                        Text = existingCandidate.Priority3,
                        Lang = Language.en,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Platform))
                {
                    details.Add(new CandidateDetail()
                    {
                        Title = "Platform",
                        Format = CandidateDetailFormat.Text,
                        Text = existingCandidate.Platform,
                        Lang = Language.en,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Biography))
                {
                    details.Add(new CandidateDetail()
                    {
                        Title = "Biography",
                        Format = CandidateDetailFormat.Text,
                        Text = existingCandidate.Biography,
                        Lang = Language.en,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Twitter))
                {
                    contacts.Add(new Contact()
                    {
                        ContactMethod = "Twitter",
                        ContactValue = existingCandidate.Twitter,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Facebook))
                {
                    contacts.Add(new Contact()
                    {
                        ContactMethod = "Facebook",
                        ContactValue = existingCandidate.Facebook,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Instagram))
                {
                    contacts.Add(new Contact()
                    {
                        ContactMethod = "Instagram",
                        ContactValue = existingCandidate.Instagram,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.YouTube))
                {
                    contacts.Add(new Contact()
                    {
                        ContactMethod = "YouTube",
                        ContactValue = existingCandidate.YouTube,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Other))
                {
                    contacts.Add(new Contact()
                    {
                        ContactMethod = "Other",
                        ContactValue = existingCandidate.Other,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Phone))
                {
                    contacts.Add(new Contact()
                    {
                        ContactMethod = "Phone",
                        ContactValue = existingCandidate.Phone,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Email))
                {
                    contacts.Add(new Contact()
                    {
                        ContactMethod = "Email",
                        ContactValue = existingCandidate.Email,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Website))
                {
                    contacts.Add(new Contact()
                    {
                        ContactMethod = "Website",
                        ContactValue = existingCandidate.Website,
                        CandidateId = candidate.CandidateId,
                    });
                }
            }

            _context.CandidateDetails.AddRange(details);
            _context.SaveChanges();

            _context.Contacts.AddRange(contacts);
            _context.SaveChanges();
        }

        private static List<Organization> GetOrganizations(List<JSONCandidate> candidateData)
        {
            List<Organization> organizations = new List<Organization>();

            foreach (var candidate in candidateData)
            {
                if (organizations.FindIndex(existingOrg => candidate.Party == existingOrg.Name) == -1)
                {
                    organizations.Add(new Organization()
                    {
                        Name = candidate.Party,
                    });
                }
            }

            return organizations;
        }

        private static List<Race> GetRaces(List<JSONCandidate> candidateData)
        {
            const int NumOfMayorsNeeded = 1;
            const int NumOfCouncillorsNeeded = 10;
            const int NumOfParkCommissionersNeeded = 7;
            const int NumOfSchoolTrusteesNeeded = 9;

            List<Race> races = new List<Race>();

            foreach (var candidate in candidateData)
            {
                if (races.FindIndex(existingRace => candidate.Position == existingRace.PositionName) == -1)
                {
                    Race race = new Race()
                    {
                        ElectionId = DummyElectionId,
                        PositionName = candidate.Position,
                    };

                    switch (candidate.Position)
                    {
                        case "Councillor":
                            race.NumberNeeded = NumOfCouncillorsNeeded;
                            break;
                        case "Mayor":
                            race.NumberNeeded = NumOfMayorsNeeded;
                            break;
                        case "Park Board commissioner":
                            race.NumberNeeded = NumOfParkCommissionersNeeded;
                            break;
                        case "School trustee":
                            race.NumberNeeded = NumOfSchoolTrusteesNeeded;
                            break;
                    }

                    races.Add(race);
                }
            }

            return races;
        }

        public static List<DataType> GetJsonData<DataType>(string filePath)
        {
            return JsonConvert.DeserializeObject<List<DataType>>(File.ReadAllText(filePath));
        }

        private static List<Election> GetElections()
        {
            return new List<Election>()
            {
                new Election()
                {
                    ElectionName = "City of Vancouver 2018 Municipal Election",
                    EndDate = "October 21 2018",
                    StartDate = "September 14 2018",
                    Description = "City of Vancouver 2018 Municipal Election"
                },
                new Election()
                {
                    ElectionName = "Canadian Federal Election 2019",
                    EndDate = "October 21 2019",
                    StartDate = "October 21 2019",
                    Description = "The 2019 Canadian federal election is scheduled to take place on or before October 21, 2019. The October 21 date of the vote is determined by the fixed-date procedures in the Canada Elections Act"
                },
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

        public static async Task InsertUserAsync(IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                UserManager<IdentityUser> userManager = serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();
                RoleManager<IdentityRole> roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

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
        }
    }
}