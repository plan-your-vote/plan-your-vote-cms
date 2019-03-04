using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VotingModelLibrary.Models
{
    public class BallotIssue
    {
        [Key]
        public int BallotIssueId { get; set; }

        public string BallotIssueTitle { get; set; }
        public string Description { get; set; }

        public List<IssueOption> BallotIssueOptions { get; set; }
    }
}
