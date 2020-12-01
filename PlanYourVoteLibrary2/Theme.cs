using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Theme
    {
        [Key]
        public string ThemeName { get; set; }

        public bool Selected { get; set; }
    }
}
