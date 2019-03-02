using System;
using System.Collections.Generic;
using System.Text;

namespace VotingModelLibrary.Models
{
    public class VoteEvent
    {
        public int VoteEventId { get; set; }
        public string VoteTitle { get; set; }
        public string VoteDescription { get; set; }
        public DateTime Date { get; set; }
        public string LogoURL { get; set; }
        public string PageBackgroundColor { get; set; }
        public string PageTextColor { get; set; }

        public int PageFontSize { get; set; }
        public string PageFontFamily { get; set; }
    }
}
