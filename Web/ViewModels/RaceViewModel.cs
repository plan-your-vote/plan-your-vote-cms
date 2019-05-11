using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.ViewModels
{
    [Authorize]
    public class RaceViewModel
    {
        public Race Race { get; set; }

        [Display(Name = "RaceCandidatesIds")]
        public List<int> RaceCandidatesIds { get; set; }
    }
}
