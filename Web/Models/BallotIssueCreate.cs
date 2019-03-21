using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingModelLibrary.Models;

namespace Web.Models
{
    public class BallotIssueCreate
    {
        public string BallotIssueTitle { get; set; }
        public string Description { get; set; }
        public List<string> OptionsTitles { get; set; }
    }
}
