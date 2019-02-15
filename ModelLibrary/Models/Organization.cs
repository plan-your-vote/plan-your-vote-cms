using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLibrary.Models
{
    public class Organization
    {
        [Key]
        string name { get; set; }
        string description { get; set; }

        List<Candidate> members { get; set; }
    }
}
