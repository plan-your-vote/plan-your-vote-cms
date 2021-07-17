using System;
namespace Web.Models.JSON
{
    public class JSONElection
    {
        public string ElectionName { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }
     }
}
