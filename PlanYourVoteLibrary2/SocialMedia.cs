using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace PlanYourVoteLibrary2

{
    public class SocialMedia
    {
        public int ID { get; set; }
        
        public int ElectionId { get; set; }

        public Election Election { get; set; }

        [Required]
        [Display(Name = "MediaName")]
        public string MediaName { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }

        [Required]
        [Display(Name = "Link")]
        public string Link { get; set; }
    }
}
