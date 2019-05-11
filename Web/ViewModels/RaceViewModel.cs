using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    [Authorize]
    public class RaceViewModel
    {
        public int ElectionId { get; set; }
        public int RaceId { get; set; }
        public string PositionName { get; set; }
        public string Description { get; set; }
        public int NumberNeeded { get; set; }
        public IList<int> RaceCandidatesIds { get; set; }
    }
}
