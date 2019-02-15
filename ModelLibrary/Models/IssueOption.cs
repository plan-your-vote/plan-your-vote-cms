using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelLibrary.Models
{
    class IssueOption
    {
        [Key][Column(Order=1)]
        string issueTitle { get; set; }
        BallotIssue issue { get; set; }
        [Key][Column(Order=2)]
        string optionTitle { get; set; }
        string optionInfo { get; set; }
    }
}
