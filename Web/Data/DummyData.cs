using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingModelLibrary.Models;

namespace Web.Data
{
    public static class DummyData
    {
        private static ApplicationDbContext _context;

        public static void Initialize(ApplicationDbContext context)
        {
            _context = context;

            context.Database.EnsureCreated();

            if (context.Candidates.Any()) { return; }

            var elections = GetElections().ToArray();
            context.Elections.AddRange(elections);
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

            var pollingStations = GetPollingStations().ToArray();
            context.PollingStations.AddRange(pollingStations);
            context.SaveChanges();
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
                    ElectionId = 2
                },
                new Race
                {
                    PositionName = "School Trustee",
                    NumberNeeded = 9,
                    ElectionId = 3
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
                    ElectionId = 1
                },
                new Candidate
                {
                    FirstName = "Mike",
                    LastName = "Hansen",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor1.jpg",
                    Biography = @"Been in housing construction most of my life, truck driver, stock promoter and various other business. Founded the Canadian Hemp Growers Assoc. in 1996. 2005 on record at Senate Committee on national defence saying, ""terrorists"" are smuggling guns/drugs into Canada. 2005 Harm Reduction Pilot Project/monopolizing heroin to save lives, Men On Down Town East Society and Two Ravens Opioid Project.",
                    OrganizationId = 2,
                    ElectionId = 2
                },
                new Candidate
                {
                    FirstName = "Wai",
                    LastName = "Young",
                    Picture = "https://vancouver.ca/plan-your-vote/img/mayor12.jpg",
                    Biography = @"Wai has lived in Vancouver for over 50 years and is a community advocate, business owner, and past Member of Parliament with over three decades of civic and policy leadership. She is a birth mother of twins and a foster parent to seven children. She holds a degree in Sociology with post graduate work in Urban Planning and Mass Communications.",
                    OrganizationId = 3,
                    ElectionId = 3
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
                    ElectionId = 1,
                    Name = "City of Vancouver 2018 Municiple Election",
                    DateEnd = "October 21 2018",
                    DateStart = "October 21 2018",
                    Description = "Voting in an election is one of the most important things a citizen can do in their community and country."
                },
                new Election()
                {
                    ElectionId = 2,
                    Name = "Canadian Federal Election, 2019",
                    DateEnd = "October 21 2019",
                    DateStart = "October 21 2019",
                    Description = "The 2019 Canadian federal election is scheduled to take place on or before October 21, 2019. The October 21 date of the vote is determined by the fixed-date procedures in the Canada Elections Act"
                },
                new Election()
                {
                    ElectionId = 3,
                    Name = "City of Vancouver 2020 Municiple Election",
                    DateEnd = "October 21 2020",
                    DateStart = "October 21 2020",
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
                    PollingStationId = 1,
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
                    PollingStationId = 2,
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
                    PollingStationId = 3,
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
                    PollingStationId = 4,
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
                    PollingStationId = 5,
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
                    PollingStationId = 6,
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
