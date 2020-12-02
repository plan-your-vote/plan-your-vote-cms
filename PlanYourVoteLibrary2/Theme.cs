using System.ComponentModel.DataAnnotations;


namespace PlanYourVoteLibrary2

{
    public class Theme
    {
        [Key]
        public string ThemeName { get; set; }

        public bool Selected { get; set; }
    }
}
