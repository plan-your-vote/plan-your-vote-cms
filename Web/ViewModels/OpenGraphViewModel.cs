using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanYourVoteLibrary2;

namespace Web.ViewModels
{
    public class OpenGraphViewModel
    {
        public OpenGraph OpenGraph;

        public string RemovedOGImages{ get; set; }
        public string RemovedOGAudios { get; set; }
        public string RemovedOGVideos { get; set; }
    }
}
