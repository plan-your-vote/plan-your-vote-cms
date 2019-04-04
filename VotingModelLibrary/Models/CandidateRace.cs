using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingModelLibrary.Models
{
    public class CandidateRace
    {
        [Key]
        public int CandidateRaceId { get; set; }

        public string PositionName { get; set; }
        public string PlatformInfo { get; set; }
        public string TopIssues { get; set; }

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }


        public int? RaceId { get; set; }

        public virtual Race Race { get; set; }
    }
}
