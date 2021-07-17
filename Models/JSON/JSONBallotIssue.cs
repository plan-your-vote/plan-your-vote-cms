using System;
namespace Web.Models.JSON
{
    public class JSONBallotIssue
    {
        public string BallotIssueTitle { get; set; }
        public string Description { get; set; }
        public int ElectionId { get; set; }
    }
}
