using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VotingModelLibrary.Models
{
    public class BallotIssue
    {
        [Key]
        public int BallotIssueId { get; set; }
         [Display(Name = "ElectionId")]
        public int ElectionId { get; set; }
        public Election Election { get; set; }
        
        [Display(Name = "BallotIssue")]
        public string BallotIssueTitle { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        public List<IssueOption> BallotIssueOptions { get; set; }
    }
}
