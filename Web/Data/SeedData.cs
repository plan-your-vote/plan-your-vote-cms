using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Web.Models;

namespace Web.Data
{
    public static class SeedData
    {
        public static ApplicationDbContext _context;

        public const int DummyElectionId = 1; // Hardcoded

        public static void Seed(this ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<Election>().HasData(GetElections().ToArray());
                modelBuilder.Entity<Organization>().HasData(GetOrganizations());
                modelBuilder.Entity<Race>().HasData(GetRaces());
                modelBuilder.Entity<Candidate>().HasData(GetCandidates());
                modelBuilder.Entity<CandidateDetail>().HasData(GetCandidateDetails());
                modelBuilder.Entity<Contact>().HasData(GetCandidateContacts());
                modelBuilder.Entity<CandidateRace>().HasData(GetCandidateRaces());
                modelBuilder.Entity<PollingPlace>().HasData(GetPollingPlaces());
                modelBuilder.Entity<PollingPlaceDate>().HasData(GetPollingPlaceDates());
                modelBuilder.Entity<BallotIssue>().HasData(GetBallotIssues());
                modelBuilder.Entity<IssueOption>().HasData(GetIssueOptions());
                modelBuilder.Entity<Step>().HasData(GetSteps());
        }

        public static List<Candidate> GetCandidates()
        {
            const string candidatesFile = "wwwroot/Data/candidates.json";
            List<JSONCandidate> candidateData = GetJsonData<JSONCandidate>(candidatesFile);
            List<Candidate> candidates = new List<Candidate>();
            foreach (JSONCandidate existingCandidate in candidateData)
            {
                candidates.Add(new Candidate {
                    CandidateId = existingCandidate.CandidateId,
                    ElectionId = DummyElectionId,
                    Name = existingCandidate.Name,
                    Picture = "images/" + existingCandidate.Picture,
                    OrganizationId = 1,
                });
            }
            return candidates;
        }

        public static List<CandidateDetail> GetCandidateDetails()
        {
            const string candidatesFile = "wwwroot/Data/candidates.json";
            List<JSONCandidate> candidateData = GetJsonData<JSONCandidate>(candidatesFile);
            List<CandidateDetail> candidateDetails = new List<CandidateDetail>();
            int id = 1;
            foreach (JSONCandidate existingCandidate in candidateData)
            {
                if (!string.IsNullOrEmpty(existingCandidate.Priority1))
                {
                    candidateDetails.Add(new CandidateDetail()
                    {
                        ID = id,
                        Title = "Priority 1",
                        Format = CandidateDetailFormat.OrderedList,
                        Text = existingCandidate.Priority1,
                        Lang = Language.en,
                        CandidateId = existingCandidate.CandidateId,
                    });
                    id++;
                }

                if (!string.IsNullOrEmpty(existingCandidate.Priority2))
                {
                    candidateDetails.Add(new CandidateDetail()
                    {
                        ID = id,
                        Title = "Priority 2",
                        Format = CandidateDetailFormat.OrderedList,
                        Text = existingCandidate.Priority2,
                        Lang = Language.en,
                        CandidateId = existingCandidate.CandidateId,
                    });
                    id++;
                }

                if (!string.IsNullOrEmpty(existingCandidate.Priority3))
                {
                    candidateDetails.Add(new CandidateDetail()
                    {
                        ID = id,
                        Title = "Priority 3",
                        Format = CandidateDetailFormat.OrderedList,
                        Text = existingCandidate.Priority3,
                        Lang = Language.en,
                        CandidateId = existingCandidate.CandidateId,
                    });
                    id++;
                }

                if (!string.IsNullOrEmpty(existingCandidate.Platform))
                {
                    candidateDetails.Add(new CandidateDetail()
                    {
                        ID = id,
                        Title = "Platform",
                        Format = CandidateDetailFormat.Text,
                        Text = existingCandidate.Platform,
                        Lang = Language.en,
                        CandidateId = existingCandidate.CandidateId,
                    });
                    id++;
                }

                if (!string.IsNullOrEmpty(existingCandidate.Biography))
                {
                    candidateDetails.Add(new CandidateDetail()
                    {
                        ID = id,
                        Title = "Biography",
                        Format = CandidateDetailFormat.Text,
                        Text = existingCandidate.Biography,
                        Lang = Language.en,
                        CandidateId = existingCandidate.CandidateId,
                    });
                    id++;
                }
            }
            return candidateDetails;
        }

        public static List<Contact> GetCandidateContacts()
        {
            const string candidatesFile = "wwwroot/Data/candidates.json";
            List<JSONCandidate> candidateData = GetJsonData<JSONCandidate>(candidatesFile);
            List<Contact> candidateContacts = new List<Contact>();
            int id = 1;
            foreach (JSONCandidate existingCandidate in candidateData)
            {
                if (!string.IsNullOrEmpty(existingCandidate.Twitter))
                {
                    candidateContacts.Add(new Contact()
                    {
                        ContactId = id,
                        ContactMethod = ContactMethod.Twitter,
                        ContactValue = existingCandidate.Twitter,
                        CandidateId = existingCandidate.CandidateId,
                    });
                    id++;
                }

                if (!string.IsNullOrEmpty(existingCandidate.Facebook))
                {
                    candidateContacts.Add(new Contact()
                    {
                        ContactId = id,
                        ContactMethod = ContactMethod.Facebook,
                        ContactValue = existingCandidate.Facebook,
                        CandidateId = existingCandidate.CandidateId,
                    });
                    id++;
                }

                if (!string.IsNullOrEmpty(existingCandidate.Instagram))
                {
                    candidateContacts.Add(new Contact()
                    {
                        ContactId = id,
                        ContactMethod = ContactMethod.Instagram,
                        ContactValue = existingCandidate.Instagram,
                        CandidateId = existingCandidate.CandidateId,
                    });
                    id++;
                }

                if (!string.IsNullOrEmpty(existingCandidate.YouTube))
                {
                    candidateContacts.Add(new Contact()
                    {
                        ContactId = id,
                        ContactMethod = ContactMethod.YouTube,
                        ContactValue = existingCandidate.YouTube,
                        CandidateId = existingCandidate.CandidateId,
                    });
                    id++;
                }

                if (!string.IsNullOrEmpty(existingCandidate.Other))
                {
                    candidateContacts.Add(new Contact()
                    {
                        ContactId = id,
                        ContactMethod = ContactMethod.Other,
                        ContactValue = existingCandidate.Other,
                        CandidateId = existingCandidate.CandidateId,
                    });
                    id++;
                }

                if (!string.IsNullOrEmpty(existingCandidate.Phone))
                {
                    candidateContacts.Add(new Contact()
                    {
                        ContactId = id,
                        ContactMethod = ContactMethod.Phone,
                        ContactValue = existingCandidate.Phone,
                        CandidateId = existingCandidate.CandidateId,
                    });
                    id++;
                }

                if (!string.IsNullOrEmpty(existingCandidate.Email))
                {
                    candidateContacts.Add(new Contact()
                    {
                        ContactId = id,
                        ContactMethod = ContactMethod.Email,
                        ContactValue = existingCandidate.Email,
                        CandidateId = existingCandidate.CandidateId,
                    });
                    id++;
                }

                if (!string.IsNullOrEmpty(existingCandidate.Website))
                {
                    candidateContacts.Add(new Contact()
                    {
                        ContactId = id,
                        ContactMethod = ContactMethod.Website,
                        ContactValue = existingCandidate.Website,
                        CandidateId = existingCandidate.CandidateId,
                    });
                    id++;
                }
            }
            return candidateContacts;
        }

        public static List<CandidateRace> GetCandidateRaces()
        {
            const string candidatesFile = "wwwroot/Data/candidates.json";
            List<JSONCandidate> candidateData = GetJsonData<JSONCandidate>(candidatesFile);
            List<CandidateRace> candidateRaces = new List<CandidateRace>();
            List<Race> races = GetRaces();
            int id = 1;
            foreach (JSONCandidate existingCandidate in candidateData)
            {
                candidateRaces.Add(new CandidateRace()
                {
                    CandidateRaceId = id,
                    CandidateId = existingCandidate.CandidateId,
                    RaceId = races.Where(race => race.PositionName == existingCandidate.Position).First().RaceId,
                    BallotOrder = int.Parse(existingCandidate.BallotOrder),
                });
                id++;
            }
            return candidateRaces;
        }
        
        public static List<PollingPlace> GetPollingPlaces()
        {
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
                    Email = ppd.Email
                })
                .ToList();
            return pollingPlaces;
        }

        public static List<PollingPlaceDate> GetPollingPlaceDates()
        {
            const string pollingPlacesFile = "wwwroot/Data/pollingPlaces.json";
            List<JSONPollingPlace> pollingPlacesData = GetJsonData<JSONPollingPlace>(pollingPlacesFile);
            List<PollingPlaceDate> pollingDates = new List<PollingPlaceDate>();
            foreach(JSONPollingPlace p in pollingPlacesData)
            {
                foreach(var ppd in p.PollingPlaceDates){
                    pollingDates.Add(new PollingPlaceDate() {
                        PollingDateId = ppd.PollingDateId,
                        PollingPlaceId = p.VotingPlaceID,
                        PollingDate = DateTime.ParseExact(ppd.PollingDate, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture),
                        StartTime = DateTime.ParseExact(ppd.StartTime, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture),
                        EndTime = DateTime.ParseExact(ppd.EndTime, "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture),
                    });
                }
            }
            return pollingDates;
        }

        private static List<Organization> GetOrganizations()
        {
            const string candidatesFile = "wwwroot/Data/candidates.json";
            List<JSONCandidate> candidateData = GetJsonData<JSONCandidate>(candidatesFile);
            List<Organization> organizations = new List<Organization>();
            int id = 1;
            foreach (var candidate in candidateData)
            {
                if (organizations.FindIndex(existingOrg => candidate.Party == existingOrg.Name) == -1)
                {
                    organizations.Add(new Organization()
                    {
                        OrganizationId = id,
                        Name = candidate.Party,
                    });
                    id++;
                }
            }

            return organizations;
        }

        private static List<Race> GetRaces()
        {
            const int NumOfMayorsNeeded = 1;
            const int NumOfCouncillorsNeeded = 10;
            const int NumOfParkCommissionersNeeded = 7;
            const int NumOfSchoolTrusteesNeeded = 9;
            const int OrderMayor = 1;
            const int OrderCouncillor = 2;
            const int OrderPark = 3;
            const int OrderSchool = 4;

            const string candidatesFile = "wwwroot/Data/candidates.json";
            List<JSONCandidate> candidateData = GetJsonData<JSONCandidate>(candidatesFile);
            List<Race> races = new List<Race>();
            int id = 1;

            foreach (var candidate in candidateData)
            {
                if (races.FindIndex(existingRace => candidate.Position == existingRace.PositionName) == -1)
                {
                    Race race = new Race()
                    {
                        RaceId = id,
                        ElectionId = DummyElectionId,
                        PositionName = candidate.Position,
                    };
                    id++;

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
                        case "Park Board Commissioner":
                            race.NumberNeeded = NumOfParkCommissionersNeeded;
                            race.BallotOrder = OrderPark;
                            break;
                        case "School Trustee":
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
                    ElectionId = 1,
                    ElectionName = "City of Vancouver 2018 Municipal Election",
                    EndDate = new DateTime(2019, 10, 21),
                    StartDate = new DateTime(2018, 9, 14),
                    Description = "City of Vancouver 2018 Municipal Election"
                },
                new Election()
                {
                    ElectionId = 2,
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
                    BallotIssueId = 1,
                    BallotIssueTitle = "1. TRANSPORTATION AND TECHNOLOGY",
                    Description = @"This question seeks authority to borrow funds to be used in carrying out the basic capital works program with respect to transportation and technology. Are you in favour of Council having the authority, without further assent of the electors, to pass bylaws between January 1, 2019, and December 31, 2022, to borrow an aggregate $100,353,000 for the following purposes?",
                    ElectionId = 1,
                },
                new BallotIssue()
                {
                    BallotIssueId = 2,
                    BallotIssueTitle = "2. CAPITAL MAINTENANCE AND RENOVATION PROGRAMS FOR EXISTING COMMUNITY FACILITIES, CIVIC FACILITIES, AND PARKS",
                    Description = @"This question seeks authority to borrow funds to be used in carrying out the basic capital works program with respect to capital maintenance and renovation programs for existing community facilities, civic facilities, and parks. Are you in favour of Council having the authority, without further assent of the electors, to pass bylaws between January 1, 2019, and December 31, 2022, to borrow an aggregate $99,557,000 for the following purposes?",
                    ElectionId = 1,
                },
                new BallotIssue()
                {
                    BallotIssueId = 3,
                    BallotIssueTitle = "3. REPLACEMENT OF EXISTING COMMUNITY FACILITIES AND CIVIC FACILITIES:",
                    Description = @"This question seeks authority to borrow funds to be used in carrying out the basic capital works program with respect to replacement of existing community facilities and civic facilities. Are you in favour of Council having the authority, without further assent of the electors, to pass bylaws between January 1, 2019, and December 31, 2022, to borrow an aggregate $100,090,000 for the following purposes?",
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
                    IssueOptionId = 1,
                    BallotIssueId = 1,
                    IssueOptionInfo = "Yes",
                },
                new IssueOption()
                {
                    IssueOptionId = 2,
                    BallotIssueId = 1,
                    IssueOptionInfo = "No",
                },
                new IssueOption()
                {
                    IssueOptionId = 3,
                    BallotIssueId = 2,
                    IssueOptionInfo = "Yes",
                },
                new IssueOption()
                {
                    IssueOptionId = 4,
                    BallotIssueId = 2,
                    IssueOptionInfo = "No",
                },
                new IssueOption()
                {
                    IssueOptionId = 5,
                    BallotIssueId = 3,
                    IssueOptionInfo = "Yes",
                },
                new IssueOption()
                {
                    IssueOptionId = 6,
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
                    ID = 1,
                    ElectionId = 1,
                    StepNumber = 1,
                    StepTitle = "STEP 1: REVIEW AND SELECT CANDIDATES",
                    StepDescription = @"Add up to 1 mayor, 10 councillors, 7 Park Board commissioners, and 9 school trustees to your plan. Open a candidate to read their profile and add them to your plan. Change your choices in the selected candidates area above. A candidate’s profile expresses their views alone and these views aren’t endorsed by the City of Vancouver. Profiles are included exactly as candidates wrote them. If you live in the UBC Lands or University Endowment Lands, and you do not own property in Vancouver, you can only vote for school trustees in the election."
                },
                new Step()
                {
                    ID = 2,
                    ElectionId = 1,
                    StepNumber = 2,
                    StepTitle = "STEP 2: REVIEW CAPITAL PLAN BORROWING QUESTIONS",
                    StepDescription = @"Add your response to the Capital Plan borrowing questions to your plan. The ballot will have 3 ""yes"" or ""no"" questions on whether the City can borrow $300 million to help pay for projects in the Capital Plan. The 2019-2022 Capital Plan invests $300,000,000 in City facilities and infrastructure to provide services to the people of Vancouver. If a majority of voters vote yes, then City Council can borrow the funds for these projects."
                },
                new Step()
                {
                    ID = 3,
                    ElectionId = 1,
                    StepNumber = 3,
                    StepTitle = "STEP 3: CHOOSE YOUR VOTING DATE AND LOCATION",
                    StepDescription = @"Not sure when you want to vote yet? Don't worry - you're not committing to a particular day or place. If you live in the UBC Lands or University Endowment Lands, you can vote at 2 voting places only on October 20 Opens in new window. These 2 places are not shown on the map below. Skip this step to review your choices and create your plan."
                },
                new Step()
                {
                    ID = 4,
                    ElectionId = 1,
                    StepNumber = 4,
                    StepTitle = "STEP 4: REVIEW YOUR PLAN",
                    StepDescription = ""
                }
            };
        }
    }
}