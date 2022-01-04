using System;
namespace Web.Models.JSON
{
    public class JSONStep
    {
        public int ElectionId { get; set; }
        public int StepNumber { get; set; }
        public string StepTitle { get; set; }
        public string StepDescription { get; set; }
    }
}
