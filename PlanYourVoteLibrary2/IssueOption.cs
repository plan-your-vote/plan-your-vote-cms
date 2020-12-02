using System.ComponentModel.DataAnnotations;


namespace PlanYourVoteLibrary2

{
    public class IssueOption
    {
        [Key]
        [Display(Name = "IssueOptionId")]
        public int IssueOptionId { get; set; }

        [Display(Name = "IssueOptionInfo")]
        public string IssueOptionInfo { get; set; }

        [Display(Name = "BallotIssueId")]
        public int BallotIssueId { get; set; }
    }
}
