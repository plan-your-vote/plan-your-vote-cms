using System.ComponentModel.DataAnnotations;

namespace VotingModelLibrary.Models
{
    public class IssueOption
    {
        [Key]
        public int IssueOptionId { get; set; }

        public string IssueOptionTitle { get; set; }
        public string IssueOptionInfo { get; set; }

        public int BallotIssueId { get; set; }
        public BallotIssue BallotIssue { get; set; }
    }
}
