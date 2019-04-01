using System.ComponentModel.DataAnnotations;

namespace VotingModelLibrary.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        [Display(Name = "ContactMethod")]
        public string ContactMethod { get; set; }   // eg. email, phone no etc
        [Display(Name = "ContactValue")]
        public string ContactValue { get; set; }  // eg. t@t.t, 555-2325 etc
        [Display(Name = "CandidateId")]
        public int CandidateId { get; set; }
        [Display(Name = "Candidate")]
        public Candidate Candidate { get; set; }
    }
}
