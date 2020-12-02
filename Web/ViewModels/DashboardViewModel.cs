using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanYourVoteLibrary2;

namespace Web.ViewModels
{
    public class DashboardViewModel
    {
        public int CandidatesCount { get; set; }
        public int ContactsCount { get; set; }
        public int BallotIssuesCount { get; set; }
        public int PollingPlacesCount { get; set; }
        public string ElectionName { get; set; }
    }
}
