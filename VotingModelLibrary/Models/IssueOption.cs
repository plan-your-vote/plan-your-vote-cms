using System.ComponentModel.DataAnnotations;

namespace VotingModelLibrary.Models
{
    public class IssueOption
    {
        [Key]
        [Display(Name = "IssueOptionId")]
        public int IssueOptionId { get; set; }
        [Display(Name = "IssueOptionTitle")]
        public string IssueOptionTitle { get; set; }
        [Display(Name = "IssueOptionInfo")]
        public string IssueOptionInfo { get; set; }
        [Display(Name = "BallotIssueId")]
        public int BallotIssueId { get; set; }
    }
}
