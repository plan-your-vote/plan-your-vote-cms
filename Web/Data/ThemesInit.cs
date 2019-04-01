using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Data;
using Web.Models;

namespace Web
{
    internal static class ThemesInit
    {
        internal static void Initialize(ApplicationDbContext context)
        {
            if (context.Theme != null && context.Theme.Any()) { return; }

            var themes = GetThemes().ToArray();
            context.Theme.AddRange(themes);
            context.SaveChanges();
        }

        private static List<Theme> GetThemes()
        {
            return new List<Theme>()
            {
                new Theme()
                {
                    ThemeName = "Default",
                    Selected = true,
                },
                new Theme()
                {
                    ThemeName = "Snowdrop",
                    Selected = false,
                },
                new Theme()
                {
                    ThemeName = "Maple",
                    Selected = false,
                },
            };
        }
    }
}