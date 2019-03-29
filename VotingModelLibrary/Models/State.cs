using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VotingModelLibrary.Models
{
    public class State
    {
        public static readonly int STATE_ID = 1;

        [Key]
        public int StateId { get; set; }
        [DisplayName("Current Election")]
        public int currentElection { get; set; }
        public Election Election { get; set; }
    }
}
