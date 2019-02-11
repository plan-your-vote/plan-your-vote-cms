using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary.Models
{
    public class CandiateRace
    {   
        int candidateId { get; set; }
        Candidate candidate { get; set; }
        string positionName { get; set; }

        string platformInfo { get; set; }
        string topIssues { get; set; }
    }
}
