using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLibrary.Models
{
    public class CandiateRace
    {   
        [Key]
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public string PositionName { get; set; }
        
        public string PlatformInfo { get; set; }
        public string IopIssues { get; set; }
    }
}
