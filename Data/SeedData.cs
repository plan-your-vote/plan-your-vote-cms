using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Web.Models;
using Web.Models.Helper;
using Web.Models.JSON;

namespace Web.Data
{
    public static class SeedData
    {
        private static IConfiguration _configuration;
        private static string importDirectory;

        public static void Initialize(ApplicationDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;

            if (!context.Elections.Any())
            {
                if (_configuration["StartupData:SourceDirectory"] != null)
                    importDirectory = _configuration["StartupData:SourceDirectory"];
                else
                    importDirectory = "wwwroot/Data/";

                InitializeDatabase(context);
            }
        }

        public static void InitializeDatabase(ApplicationDbContext _context)
        {
            string candidatesFile;
            string absoluteFileName;

            var elections = GetElections(null);
            _context.Elections.AddRange(elections);
            _context.SaveChanges();

            var pollingPlaces = GetPollingPlaces(null);
            _context.PollingPlaces.AddRange(pollingPlaces);
            _context.SaveChanges();

            if (_configuration["StartupData:CandidatesFile"] != null)
                candidatesFile = _configuration["StartupData:CandidatesFile"];
            else
                candidatesFile = "candidates.json";

            absoluteFileName = importDirectory + candidatesFile;
            List<JSONCandidate> candidateData = GetJsonData<JSONCandidate>(absoluteFileName);

            var organizations = GetOrganizations(_context, candidateData);
            _context.Organizations.AddRange(organizations);
            _context.SaveChanges();

            var races = GetRaces(_context, candidateData);
            _context.Races.AddRange(races);
            _context.SaveChanges();

            GetCandidatesAndContacts(_context, candidateData);

            var ballotIssues = GetBallotIssues(null);
            _context.BallotIssues.AddRange(ballotIssues);
            _context.SaveChanges();

            var issueOptions = GetIssueOptions(null);
            _context.IssueOptions.AddRange(issueOptions);
            _context.SaveChanges();

            var steps = GetSteps(null);
            _context.Steps.AddRange(steps);
            _context.SaveChanges();
        }

        public static void GetCandidatesAndContacts(ApplicationDbContext _context, List<JSONCandidate> candidateData)
        {
            List<Contact> contacts = new List<Contact>();
            List<CandidateDetail> details = new List<CandidateDetail>();
            List<CandidateRace> candidateRaces = new List<CandidateRace>();
            int dummyElectionId;

            if (_configuration["StartupData:DummyElectionId"] != null)
                dummyElectionId = Convert.ToInt32(_configuration["StartupData:DummyElectionId"]);
            else
                dummyElectionId = 1;

            foreach (var existingCandidate in candidateData)
            {
                Candidate candidate = new Candidate()
                {
                    ElectionId = dummyElectionId,
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

        public static List<Organization> GetOrganizations(ApplicationDbContext _context, List<JSONCandidate> candidateData)
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

        public static List<Race> GetRaces(ApplicationDbContext _context, List<JSONCandidate> candidateData)
        {
            int dummyElectionId;

            int NumOfMayorsNeeded = Convert.ToInt32(SettingsConfigHelper.AppSetting("CurrentRace:NumOfMayorsNeeded"));
            int NumOfCouncillorsNeeded = Convert.ToInt32(SettingsConfigHelper.AppSetting("CurrentRace:NumOfCouncillorsNeeded"));
            int NumOfParkCommissionersNeeded = Convert.ToInt32(SettingsConfigHelper.AppSetting("CurrentRace:NumOfParkCommissionersNeeded"));
            int NumOfSchoolTrusteesNeeded = Convert.ToInt32(SettingsConfigHelper.AppSetting("CurrentRace:NumOfSchoolTrusteesNeeded"));
            int OrderMayor = Convert.ToInt32(SettingsConfigHelper.AppSetting("CurrentRace:OrderMayor"));
            int OrderCouncillor = Convert.ToInt32(SettingsConfigHelper.AppSetting("CurrentRace:OrderCouncillor"));
            int OrderPark = OrderCouncillor = Convert.ToInt32(SettingsConfigHelper.AppSetting("CurrentRace:OrderPark"));
            int OrderSchool = Convert.ToInt32(SettingsConfigHelper.AppSetting("CurrentRace:OrderSchool"));

            if (_configuration["StartupData:DummyElectionId"] != null)
                dummyElectionId = Convert.ToInt32(_configuration["StartupData:DummyElectionId"]);
            else
                dummyElectionId = 1;

            List<Race> races = new List<Race>();

            foreach (var candidate in candidateData)
            {
                if (races.FindIndex(existingRace => candidate.Position == existingRace.PositionName) == -1)
                {
                    Race race = new Race()
                    {
                        ElectionId = dummyElectionId,
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

        public static List<Election> GetElections(string filename)
        {
            string file;
            string absoluteFileName;

            if (filename == null)
            {
                if (_configuration["StartupData:ElectionsFile"] != null)
                    file = _configuration["StartupData:ElectionsFile"];
                else
                    file = "elections.json";

                absoluteFileName = importDirectory + file;
            }
            else
            {
                absoluteFileName = filename;
            }

            List<JSONElection> electionData = GetJsonData<JSONElection>(absoluteFileName);

            List<Election> elections = new List<Election>();

            foreach (var item in electionData)
            {

                elections.Add(new Election()
                {
                    ElectionName = item.ElectionName,
                    EndDate = item.EndDate,
                    StartDate = item.StartDate,
                    Description = item.Description
                });
            }

            return elections;

        }

        public static List<BallotIssue> GetBallotIssues(string filename)
        {
            string file;
            string absoluteFileName;

            if (filename == null)
            {

                if (_configuration["StartupData:BallotIssuesFile"] != null)
                    file = _configuration["StartupData:BallotIssuesFile"];
                else
                    file = "elections.json";

                absoluteFileName = importDirectory + file;
            }
            else
            {
                absoluteFileName = filename;
            }

            List<JSONBallotIssue> data = GetJsonData<JSONBallotIssue>(absoluteFileName);

            List<BallotIssue> list = new List<BallotIssue>();

            foreach (var item in data)
            {

                list.Add(new BallotIssue()
                {
                    BallotIssueTitle = item.BallotIssueTitle,
                    Description = item.Description,
                    ElectionId = item.ElectionId
                });
            }

            return list;
        }

        public static List<PollingPlace> GetPollingPlaces(string filename)
        {
            string file;
            string absoluteFileName;
            int dummyElectionId;

            if (filename == null)
            {
                if (_configuration["StartupData:PollingPlacesFile"] != null)
                    file = _configuration["StartupData:PollingPlacesFile"];
                else
                    file = "polling_places.json";

                absoluteFileName = importDirectory + file;
            }
            else
            {
                absoluteFileName = filename;
            }

            List<JSONPollingPlace> data = GetJsonData<JSONPollingPlace>(absoluteFileName);

            if (_configuration["StartupData:DummyElectionId"] != null)
                dummyElectionId = Convert.ToInt32(_configuration["StartupData:DummyElectionId"]);
            else
                dummyElectionId = 1;

            List<PollingPlace> list = data
               .Select(ppd => new PollingPlace()
               {
                   ElectionId = dummyElectionId,
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


            return list;
        }


        public static List<IssueOption> GetIssueOptions(string filename)
        {
            string file;
            string absoluteFileName;

            if (filename == null)
            {
                if (_configuration["StartupData:IssueOptionsFile"] != null)
                    file = _configuration["StartupData:IssueOptionsFile"];
                else
                    file = "issue_options.json";

                absoluteFileName = importDirectory + file;
            }
            else
            {
                absoluteFileName = filename;
            }

            List<JSONIssueOption> data = GetJsonData<JSONIssueOption>(absoluteFileName);

            List<IssueOption> list = new List<IssueOption>();

            foreach (var item in data)
            {

                list.Add(new IssueOption()
                {
                    BallotIssueId = item.BallotIssueId,
                    IssueOptionInfo = item.IssueOptionInfo,
                });
            }

            return list;

        }

        public static List<Step> GetSteps(string filename)
        {
 
            string file;
            string absoluteFileName;

            if (filename == null)
            {
                if (_configuration["StartupData:StepsFile"] != null)
                    file = _configuration["StartupData:StepsFile"];
                else
                    file = "steps.json";

                absoluteFileName = importDirectory + file;
            } else
            {
                absoluteFileName = filename;
            }            

            List<JSONStep> data = GetJsonData<JSONStep>(absoluteFileName);

            List<Step> list = new List<Step>();

            foreach (var item in data)
            {

                list.Add(new Step()
                {
                    ElectionId = item.ElectionId,
                    StepNumber = item.StepNumber,
                    StepTitle = item.StepTitle,
                    StepDescription = item.StepDescription
                });
            }

            return list;
        }
    }
}