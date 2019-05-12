using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class BallotIssueViewModel
    {
        [Display(Name = "Ballot Issue")]
        public string BallotIssueTitle { get; set; }

        public string Description { get; set; }

        public List<string> OptionsTitles { get; set; }
    }
}
