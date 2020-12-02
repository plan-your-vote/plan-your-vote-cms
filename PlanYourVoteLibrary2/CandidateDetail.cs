using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanYourVoteLibrary2

{
    public enum CandidateDetailFormat
    {
        [Display(Name = "Text")]
        Text,
        [Display(Name = "UnorderedList")]
        List,
        [Display(Name = "OrderedList")]
        OrderedList
    }

    public enum Language
    {
        [Display(Name = "en")]
        en,
        [Display(Name = "fr")]
        fr
    }

    public class CandidateDetail
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "CandidateId")]
        public int CandidateId { get; set; }

        [Display(Name = "Candidate")]
        public Candidate Candidate { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "Format")]
        public CandidateDetailFormat Format { get; set; }

        [Display(Name = "Language")]
        public Language Lang { get; set; }
    }
}
