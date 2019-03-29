using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VotingModelLibrary.Models
{
    class Election
    {
        [Key]
        public int ElectionId { get; set; }

        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
