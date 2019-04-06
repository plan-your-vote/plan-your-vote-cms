using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingModelLibrary.Models
{
    public class CandidateRace
    {
        [Key]
        [Display(Name = "CandidateRaceId")]
        public int CandidateRaceId { get; set; }
        [Display(Name = "PositionName")]
        public string PositionName { get; set; }
        [Display(Name = "PlatformInfo")]
        public string PlatformInfo { get; set; }
        [Display(Name = "TopIssues")]
        public string TopIssues { get; set; }
        [Display(Name = "CandidateId")]
        public int CandidateId { get; set; }
        [Display(Name = "Candidate")]
        public Candidate Candidate { get; set; }

        [Display(Name = "RaceId")]
        public int? RaceId { get; set; }
        [Display(Name = "Race")]
        public virtual Race Race { get; set; }
    }
}
