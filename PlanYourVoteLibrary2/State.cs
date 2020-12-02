using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace PlanYourVoteLibrary2

{
    public class State
    {
        public static readonly int STATE_ID = 1;

        [Key]
        [DisplayName("StateId")]
        public int StateId { get; set; }

        [DisplayName("RunningElectionID")]
        public int RunningElectionID { get; set; }

        [DisplayName("Running Election")]
        public Election RunningElection { get; set; }

        [DisplayName("ManagedElectionID")]
        public int ManagedElectionID { get; set; }

        [DisplayName("Managed Election")]
        public Election ManagedElection { get; set; }
    }
}
