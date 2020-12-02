using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace PlanYourVoteLibrary2

{
    public class Image
    {
        public int ID { get; set; }

        [Display(Name = "ThemeName")]
        [Required(ErrorMessage = "Please enter the theme to associate this image with.")]
        public string ThemeName { get; set; }

        [Display(Name = "Placement")]
        [Required(ErrorMessage = "Please enter the area to place the image, eg. Logo, Footer Logo, etc.")]
        public string Placement { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Please enter the url that the image will link to.")]

        [Display(Name = "Value")]
        public string Value { get; set; }

        [Display(Name = "Format")]
        public string Format { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
