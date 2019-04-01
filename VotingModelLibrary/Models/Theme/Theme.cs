using System.ComponentModel.DataAnnotations;

namespace VotingModelLibrary.Models.Theme
{
    public class Theme
    {
        [Key]
        public string ThemeName { get; set; }

        public bool Selected { get; set; }
    }
}
