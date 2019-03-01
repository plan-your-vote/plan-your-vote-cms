using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelLibrary.Models
{
    public class IssueOption
    {
        public string IssueTitle { get; set; }
        public BallotIssue Issue { get; set; }
        public string OptionTitle { get; set; }
        public string OptionInfo { get; set; }
    }
}
