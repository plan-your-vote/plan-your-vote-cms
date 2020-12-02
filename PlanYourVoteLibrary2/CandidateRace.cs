using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PlanYourVoteLibrary2

{
    public class CandidateRace
    {
        [Key]
        [Display(Name = "CandidateRaceId")]
        public int CandidateRaceId { get; set; }

        [ForeignKey("CandidateId")]
        [Display(Name = "CandidateId")]
        public int CandidateId { get; set; }

        [Display(Name = "Candidate")]
        public Candidate Candidate { get; set; }

        [ForeignKey("RaceId")]
        [Display(Name = "RaceId")]
        public int RaceId { get; set; }

        [Display(Name = "Race")]
        public Race Race { get; set; }

        [Display(Name = "BallotOrder")]
        [Range(1, Int32.MaxValue)]
        public int BallotOrder { get; set; }
    }
}
