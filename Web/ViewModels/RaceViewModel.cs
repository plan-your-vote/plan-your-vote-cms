using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PlanYourVoteLibrary2;

namespace Web.ViewModels
{
    [Authorize]
    public class RaceViewModel
    {
        public Race Race { get; set; }

        public SelectList Candidates { get; set; }

        public List<string> CandidateIds { get; set; }
    }
}
