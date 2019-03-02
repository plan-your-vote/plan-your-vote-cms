using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelLibrary.Models
{
    public class Contact
    {   
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public string Type { get; set; }   // eg. email, phone no etc
        public string Value { get; set; }  // eg. t@t.t, 555-2325 etc
    }
}
