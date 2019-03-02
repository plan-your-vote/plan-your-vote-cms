using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLibrary.Models
{
    public class Organization
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }

        List<Candidate> Members { get; set; }
    }
}
