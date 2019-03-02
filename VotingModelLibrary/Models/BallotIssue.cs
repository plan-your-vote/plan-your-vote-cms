using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLibrary.Models
{
    public class BallotIssue
    {   
        [Key]
        public string IssueTitle { get; set; }
        public string Description { get; set; }
        
        ICollection<IssueOption> options { get; set; }
    }
}
