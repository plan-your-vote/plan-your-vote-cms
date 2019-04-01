using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VotingModelLibrary.Models.Theme
{
    public class Image
    {
        /* Composite key via Fluent API - https://docs.microsoft.com/en-us/ef/core/modeling/keys
         * e.g. modelBuilder.Entity<Car>().HasKey(c => new { c.State, c.LicensePlate });
         */
        public string ThemeName { get; set; }
        public string ID { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Format { get; set; }
        public string Description { get; set; }
    }
}
