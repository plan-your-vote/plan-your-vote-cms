using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelLibrary.Models
{
    class Contact
    {   
        [Key][Column(Order=1)]
        int candidateId { get; set; }
        Candidate candidate { get; set; }
        [Key][Column(Order = 2)]
        string type { get; set; }   // eg. email, phone no etc
        string value { get; set; }  // eg. t@t.t, 555-2325 etc
    }
}
