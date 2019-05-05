using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class SocialMedia
    {
        public int ID { get; set; }
        [Display(Name = "MediaName")]
        public string MediaName { get; set; }
        [Display(Name = "Message")]
        public string Message { get; set; }
        [Display(Name = "Link")]
        public string Link { get; set; }
    }
}
