using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLibrary.Models
{
    public class BallotIssue
    {
        [Key]
        string issueTitle { get; set; }
        string desciption { get; set; }
        
        List<IssueOption> options { get; set; }
    }
}
