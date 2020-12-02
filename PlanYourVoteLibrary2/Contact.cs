using System.ComponentModel.DataAnnotations;

namespace PlanYourVoteLibrary2

{
    public enum ContactMethod
    {
        [Display(Name = "Phone")]
        Phone,
        [Display(Name = "Email")]
        Email,
        [Display(Name = "Twitter")]
        Twitter,
        [Display(Name = "Facebook")]
        Facebook,
        [Display(Name = "Instagram")]
        Instagram,
        [Display(Name = "LinkedIn")]
        LinkedIn,
        [Display(Name = "YouTube")]
        YouTube,
        [Display(Name = "Website")]
        Website,
        [Display(Name = "Other")]
        Other
    }
    public class Contact
    {
        [Key]
        [Display(Name = "ContactId")]
        public int ContactId { get; set; }

        [Display(Name = "ContactMethod")]
        public ContactMethod ContactMethod { get; set; }   // eg. email, phone no etc

        [Display(Name = "ContactValue")]
        public string ContactValue { get; set; }  // eg. t@t.t, 555-2325 etc

        [Display(Name = "CandidateId")]
        public int CandidateId { get; set; }

        [Display(Name = "Candidate")]
        public Candidate Candidate { get; set; }
    }
}
