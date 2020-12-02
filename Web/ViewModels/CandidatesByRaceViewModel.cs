using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanYourVoteLibrary2;

namespace Web.ViewModels
{
    public class CandidatesByRaceViewModel
    {
        public IList<Race> Races { get; set; }
        public List<IGrouping<int,CandidateRace>> CandidatesByRace { get; set; }
        public IList<Candidate> UnlistedCandidates { get; set; }
    }
}
