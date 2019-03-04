using System.ComponentModel.DataAnnotations;

namespace VotingModelLibrary.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        public string ContactMethod { get; set; }   // eg. email, phone no etc
        public string ContactValue { get; set; }  // eg. t@t.t, 555-2325 etc

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
