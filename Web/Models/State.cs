using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Web.Models
{
    public class State
    {
        public static readonly int STATE_ID = 1;

        [Key]
        [DisplayName("StateId")]
        public int StateId { get; set; }

        [DisplayName("CurrentElection")]
        public int CurrentElection { get; set; }

        [DisplayName("Election")]
        public Election Election { get; set; }
    }
}
