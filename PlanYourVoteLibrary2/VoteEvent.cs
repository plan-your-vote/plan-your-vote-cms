using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlanYourVoteLibrary2

{
    public class VoteEvent
    {
        [Display(Name = "VoteEventId")]
        public int VoteEventId { get; set; }

        [Display(Name = "VoteTitle")]
        public string VoteTitle { get; set; }

        [Display(Name = "VoteDesc")]
        public string VoteDescription { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "LogoURL")]
        public string LogoURL { get; set; }

        [Display(Name = "PageBackgroundColor")]
        public string PageBackgroundColor { get; set; }

        [Display(Name = "PageTextColor")]
        public string PageTextColor { get; set; }

        [Display(Name = "PageFontSize")]
        public int PageFontSize { get; set; }

        [Display(Name = "PageFontFamily")]
        public string PageFontFamily { get; set; }
    }
}
