using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using PlanYourVoteLibrary2;

namespace Web.Data
{
    public static class SeedData
    {
        public static ApplicationDbContext _context;

        public const int DummyElectionId = 1; // Hardcoded

        public static void Initialize(ApplicationDbContext context)
        {
            _context = context;

            InitializeDatabase();
        }

        public static void InitializeDatabase()
        {
            const string candidatesFile = "wwwroot/Data/candidates.json";
            List<JSONCandidate> candidateData = GetJsonData<JSONCandidate>(candidatesFile);

            var elections = GetElections().ToArray();
            _context.Elections.AddRange(elections);
            _context.SaveChanges();

            const string pollingPlacesFile = "wwwroot/Data/pollingPlaces.json";
            List<JSONPollingPlace> pollingPlacesData = GetJsonData<JSONPollingPlace>(pollingPlacesFile);

            List<PollingPlace> pollingPlaces = pollingPlacesData
                .Select(ppd => new PollingPlace()
                {
                    ElectionId = DummyElectionId,
                    PollingPlaceId = ppd.VotingPlaceID,
                    PollingPlaceName = ppd.FacilityName,
                    Address = ppd.FacilityAddress,
                    PollingStationName = ppd.Location,
                    Latitude = ppd.Latitude,
                    Longitude = ppd.Longitude,
                    AdvanceOnly = ppd.AdvanceOnly,
                    LocalArea = ppd.LocalArea,
                    WheelchairInfo = ppd.WheelchairAccess,
                    ParkingInfo = ppd.Parking,
                    Phone = ppd.Phone,
                    Email = ppd.Email,
                    PollingPlaceDates = ppd.PollingPlaceDates.Select(jsppd => new PollingPlaceDate()
                    {
                        PollingDate = DateTime.ParseExact(jsppd.PollingDate, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture),
                        StartTime = DateTime.ParseExact(jsppd.StartTime, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture),
                        EndTime = DateTime.ParseExact(jsppd.EndTime, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture),
                    }).ToList(),
                })
                .ToList();
            _context.PollingPlaces.AddRange(pollingPlaces);
            _context.SaveChanges();

            var organizations = GetOrganizations(candidateData).ToArray();
            _context.Organizations.AddRange(organizations);
            _context.SaveChanges();

            var races = GetRaces(candidateData).ToArray();
            _context.Races.AddRange(races);
            _context.SaveChanges();

            GetCandidatesAndContacts(candidateData);

            var ballotIssues = GetBallotIssues().ToArray();
            _context.BallotIssues.AddRange(ballotIssues);
            _context.SaveChanges();

            var issueOptions = GetIssueOptions().ToArray();
            _context.IssueOptions.AddRange(issueOptions);
            _context.SaveChanges();

            var steps = GetSteps().ToArray();
            _context.Steps.AddRange(steps);
            _context.SaveChanges();
        }

        private static void GetCandidatesAndContacts(List<JSONCandidate> candidateData)
        {
            List<Contact> contacts = new List<Contact>();
            List<CandidateDetail> details = new List<CandidateDetail>();
            List<CandidateRace> candidateRaces = new List<CandidateRace>();

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
                        ContactMethod = ContactMethod.Twitter,
                        ContactValue = existingCandidate.Twitter,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Facebook))
                {
                    contacts.Add(new Contact()
                    {
                        ContactMethod = ContactMethod.Facebook,
                        ContactValue = existingCandidate.Facebook,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Instagram))
                {
                    contacts.Add(new Contact()
                    {
                        ContactMethod = ContactMethod.Instagram,
                        ContactValue = existingCandidate.Instagram,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.YouTube))
                {
                    contacts.Add(new Contact()
                    {
                        ContactMethod = ContactMethod.YouTube,
                        ContactValue = existingCandidate.YouTube,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Other))
                {
                    contacts.Add(new Contact()
                    {
                        ContactMethod = ContactMethod.Other,
                        ContactValue = existingCandidate.Other,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Phone))
                {
                    contacts.Add(new Contact()
                    {
                        ContactMethod = ContactMethod.Phone,
                        ContactValue = existingCandidate.Phone,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Email))
                {
                    contacts.Add(new Contact()
                    {
                        ContactMethod = ContactMethod.Email,
                        ContactValue = existingCandidate.Email,
                        CandidateId = candidate.CandidateId,
                    });
                }

                if (!string.IsNullOrEmpty(existingCandidate.Website))
                {
                    contacts.Add(new Contact()
                    {
                        ContactMethod = ContactMethod.Website,
                        ContactValue = existingCandidate.Website,
                        CandidateId = candidate.CandidateId,
                    });
                }

                CandidateRace candidateRace = new CandidateRace()
                {
                    CandidateId = candidate.CandidateId,
                    RaceId = _context.Races
                    .Where(races => races.PositionName == existingCandidate.Position)
                    .First()
                    .RaceId,
                    BallotOrder = int.Parse(existingCandidate.BallotOrder),
                };

                candidateRaces.Add(candidateRace);
            }

            _context.CandidateRaces.AddRange(candidateRaces);
            _context.SaveChanges();

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
            const int OrderMayor = 1;
            const int OrderCouncillor = 2;
            const int OrderPark = 3;
            const int OrderSchool = 4;

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
                            race.BallotOrder = OrderCouncillor;
                            break;
                        case "Mayor":
                            race.NumberNeeded = NumOfMayorsNeeded;
                            race.BallotOrder = OrderMayor;
                            break;
                        case "Park Board commissioner":
                            race.NumberNeeded = NumOfParkCommissionersNeeded;
                            race.BallotOrder = OrderPark;
                            break;
                        case "School trustee":
                            race.NumberNeeded = NumOfSchoolTrusteesNeeded;
                            race.BallotOrder = OrderSchool;
                            break;
                    }

                    races.Add(race);
                }
            }

            return races;
        }

        public static List<DataType> GetJsonData<DataType>(string filePath)
        {
            List<DataType> data = null;

            using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
            {
                data = JsonConvert.DeserializeObject<List<DataType>>(streamReader.ReadToEnd());
            }

            return data;
        }

        private static List<Election> GetElections()
        {
            return new List<Election>()
            {
                new Election()
                {
                    ElectionName = "City of Vancouver 2018 Municipal Election",
                    EndDate = new DateTime(2019, 10, 21),
                    StartDate = new DateTime(2018, 9, 14),
                    Description = "City of Vancouver 2018 Municipal Election"
                },
                new Election()
                {
                    ElectionName = "Canadian Federal Election 2019",
                    EndDate = new DateTime(2019, 10, 21),
                    StartDate = new DateTime(2019, 10, 21),
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
                    BallotIssueTitle = "1. TRANSPORTATION AND TECHNOLOGY",
                    Description = @"This question seeks authority to borrow funds to be used in carrying out the basic capital works program with respect to transportation and technology.

Are you in favour of Council having the authority, without further assent of the electors, to pass bylaws between January 1, 2019, and December 31, 2022, to borrow an aggregate $100,353,000 for the following purposes?",
                    ElectionId = 1,
                },
                new BallotIssue()
                {
                    BallotIssueTitle = "2. CAPITAL MAINTENANCE AND RENOVATION PROGRAMS FOR EXISTING COMMUNITY FACILITIES, CIVIC FACILITIES, AND PARKS",
                    Description = @"This question seeks authority to borrow funds to be used in carrying out the basic capital works program with respect to capital maintenance and renovation programs for existing community facilities, civic facilities, and parks.

Are you in favour of Council having the authority, without further assent of the electors, to pass bylaws between January 1, 2019, and December 31, 2022, to borrow an aggregate $99,557,000 for the following purposes?",
                    ElectionId = 1,
                },
                new BallotIssue()
                {
                    BallotIssueTitle = "3. REPLACEMENT OF EXISTING COMMUNITY FACILITIES AND CIVIC FACILITIES:",
                    Description = @"This question seeks authority to borrow funds to be used in carrying out the basic capital works program with respect to replacement of existing community facilities and civic facilities.

Are you in favour of Council having the authority, without further assent of the electors, to pass bylaws between January 1, 2019, and December 31, 2022, to borrow an aggregate $100,090,000 for the following purposes?",
                    ElectionId = 1,
                }
            };
        }

        private static List<IssueOption> GetIssueOptions()
        {
            return new List<IssueOption>()
            {
                new IssueOption()
                {
                    BallotIssueId = 1,
                    IssueOptionInfo = "Yes",
                },
                new IssueOption()
                {
                    BallotIssueId = 1,
                    IssueOptionInfo = "No",
                },
                new IssueOption()
                {
                    BallotIssueId = 2,
                    IssueOptionInfo = "Yes",
                },
                new IssueOption()
                {
                    BallotIssueId = 2,
                    IssueOptionInfo = "No",
                },
                new IssueOption()
                {
                    BallotIssueId = 3,
                    IssueOptionInfo = "Yes",
                },
                new IssueOption()
                {
                    BallotIssueId = 3,
                    IssueOptionInfo = "No",
                },
            };
        }

        private static List<Step> GetSteps()
        {
            return new List<Step>()
            {
                new Step()
                {
                    ElectionId = 1,
                    StepNumber = 1,
                    StepTitle = "STEP 1: REVIEW AND SELECT CANDIDATES",
                    StepDescription = @"Add up to 1 mayor, 10 councillors, 7 Park Board commissioners, and 9 school trustees to your plan. Open a candidate to read their profile and add them to your plan. Change your choices in the selected candidates area above.

A candidate’s profile expresses their views alone and these views aren’t endorsed by the City of Vancouver. Profiles are included exactly as candidates wrote them.

If you live in the UBC Lands or University Endowment Lands, and you do not own property in Vancouver, you can only vote for school trustees in the election."
                },
                new Step()
                {
                    ElectionId = 1,
                    StepNumber = 2,
                    StepTitle = "STEP 2: REVIEW CAPITAL PLAN BORROWING QUESTIONS",
                    StepDescription = @"Add your response to the Capital Plan borrowing questions to your plan.

The ballot will have 3 ""yes"" or ""no"" questions on whether the City can borrow $300 million to help pay for projects in the Capital Plan.

The 2019-2022 Capital Plan invests $300,000,000 in City facilities and infrastructure to provide services to the people of Vancouver.

If a majority of voters vote yes, then City Council can borrow the funds for these projects."
                },
                new Step()
                {
                    ElectionId = 1,
                    StepNumber = 3,
                    StepTitle = "STEP 3: CHOOSE YOUR VOTING DATE AND LOCATION",
                    StepDescription = @"Not sure when you want to vote yet? Don't worry - you're not committing to a particular day or place. If you live in the UBC Lands or University Endowment Lands, you can vote at 2 voting places only on October 20 Opens in new window. These 2 places are not shown on the map below. Skip this step to review your choices and create your plan."
                },
                new Step()
                {
                    ElectionId = 1,
                    StepNumber = 4,
                    StepTitle = "STEP 4: REVIEW YOUR PLAN",
                    StepDescription = ""
                }
            };
        }
    }
}