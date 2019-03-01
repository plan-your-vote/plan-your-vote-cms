using Microsoft.EntityFrameworkCore;
using ModelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Data
{
    public class DummyData
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Candidate.Any()) { return; }

            var candidates = DummyData.GetCandidate().ToArray();
            var contacts = DummyData.GetContact().ToArray();
            var races = DummyData.GetRace().ToArray();
            var organizations = DummyData.GetOrganisation().ToArray();
            var issueOptions = DummyData.GetIssueOption().ToArray();
            var candiateRaces = DummyData.GetCandidateRace().ToArray();
            var ballotIssues = DummyData.GetBallotIssue().ToArray();

            //context.Candid.AddRange(boats);
            context.SaveChanges();
        }

        public static List<Candidate> GetCandidate()
        {
        //    List<Boat> boats = new List<Boat>() {
        //        new Boat {BoatName="Alpha Centauri",
        //            Description ="Lorem ipsum dolor sit amet, <strong>consectetur</strong> adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore <em>magna aliqua.</em>",
        //            Picture ="/assets/img/boat1.jpeg", Make="Centaurus", LengthInFeet="20"}
        //    };
            return null;
        }
        public static List<Contact> GetContact()
        {
            return null;
        }

        public static List<Race> GetRace()
        {
            return null;
        }

        public static List<Organization> GetOrganisation()
        {
            return null;
        }

        public static List<IssueOption> GetIssueOption()
        {
            return null;
        }

        public static List<CandiateRace> GetCandidateRace()
        {
            return null;
        }

        public static List<BallotIssue> GetBallotIssue()
        {
            return null;
        }

    }
}
