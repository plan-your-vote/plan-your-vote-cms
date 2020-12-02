using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlanYourVoteLibrary2;

namespace Web.ViewModels
{
    public class CandidateViewModel
    {
        public Candidate Candidate { get; set; }
        public IFormFile Image { get; set; }
        public SelectList Organizations { get; set; }
        public SelectList Races { get; set; }
        public string RemovedDetails { get; set; }
        public string RemovedContacts { get; set; }
        public List<string> RaceIds { get; set; }
    }
}
