using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlanYourVoteLibrary2

{
    public class BallotIssue
    {
        [Key]
        [Display(Name = "BallotIssueId")]
        public int BallotIssueId { get; set; }

        [Display(Name = "ElectionId")]
        public int ElectionId { get; set; }

        [Display(Name = "Election")]
        public Election Election { get; set; }

        [Display(Name = "BallotIssue")]
        public string BallotIssueTitle { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "BallotIssueOptions")]
        public List<IssueOption> BallotIssueOptions { get; set; }
    }
}
