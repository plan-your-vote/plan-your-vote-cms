using System;
using System.Collections.Generic;
using System.Linq;
using VotingModelLibrary.Models.Theme;
using Web.Data;

namespace Web
{
    internal static class ThemesInit
    {
        internal static void Initialize(ApplicationDbContext context)
        {
            if (context.Themes?.Any() != true)
            {
                var themes = GetThemes().ToArray();
                context.Themes.AddRange(themes);
                context.SaveChangesAsync();
            }

            if (context.Images?.Any() != true)
            {
                var images = GetImages().ToArray();
                context.Images.AddRange(images);
                context.SaveChangesAsync();
            }
        }

        private static List<Image> GetImages()
        {
            return new List<Image>()
            {
                new Image()
                {
                    ThemeName = "Default",
                    ID = "PYV Logo",
                    Type = "URL",
                    Value = "https://vancouver.ca/plan-your-vote/img/logo_home.png",
                    Format = "PNG",
                    Description = "Vancouver Votes",
                },
                new Image()
                {
                    ThemeName = "Default",
                    ID = "Vancouver Logo",
                    Type = "URL",
                    Value = "https://vancouver.ca/plan-your-vote/img/cov_logo.png",
                    Format = "PNG",
                    Description = "City of Vancouver",
                },
                new Image()
                {
                    ThemeName = "Snowdrop",
                    ID = "BCIT Logo",
                    Type = "Path",
                    Value = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHZpZXdCb3g9IjAgMCAxNDcuNiAxMzMuNiI+PHN0eWxlIHR5cGU9InRleHQvY3NzIj4uc3Qwe2ZpbGw6IzAwM0U2Qjt9LnN0MXtmaWxsOiNGRkZGRkY7fTwvc3R5bGU+PHJlY3QgY2xhc3M9InN0MCIgd2lkdGg9IjEzMy41IiBoZWlnaHQ9IjEzMy42Ii8+PHBhdGggY2xhc3M9InN0MCIgZD0iTTEzNi4yIDkuNlYyLjVoLTEuN1YxaDUuMXYxLjVoLTEuN3Y3LjFIMTM2LjJ6TTE0NiA5LjZ2LTZoMGwtMS4zIDZoLTEuMmwtMS40LTZoMHY2aC0xLjVWMWgyLjNsMS4yIDUuNGgwbDEuMy01LjRoMi4ydjguNkgxNDZ6Ii8+PHBhdGggY2xhc3M9InN0MSIgZD0iTTI1LjQgODguN2M4LjkgMCAxNS44LTIuNiAxNS44LTEzLjIgMC01LjItMS45LTkuMS03LTEwLjl2LTAuMWMzLjgtMS43IDUuOS00LjkgNS45LTkuNyAwLTguNS00LjgtMTEuOS0xNS4zLTExLjlIMTMuNXY0NS45SDI1LjR6TTY3LjQgNzNjLTEgNi4zLTMuMSA5LjUtNi44IDkuNSAtNC45IDAtNy00LjMtNy0xN0M1My42IDUzLjkgNTYgNDkgNjAuNyA0OWMzLjQgMCA1LjQgMi45IDYuMyA4LjdsNy0wLjZjLTEuMS05LjMtNC43LTE0LjgtMTMuNS0xNC44IC0xMC45IDAtMTQuNyAxMC4xLTE0LjcgMjMuMyAwIDE0LjggMy42IDIzLjcgMTQuNiAyMy43IDguNCAwIDEyLjYtNS4zIDE0LjEtMTUuNUw2Ny40IDczek04OC41IDg4LjdWNDIuOGgtNy42djQ1LjlIODguNXpNMTExLjcgODguN3YtMzloOS41di02LjlIOTQuN3Y2LjloOS41djM5SDExMS43eiIvPjxwYXRoIGNsYXNzPSJzdDAiIGQ9Ik0yMC45IDYxLjhWNDkuNmg0LjJjMy43IDAgNy4zIDAuOSA3LjMgNS44IDAgNS0zLjQgNi41LTcuMyA2LjVIMjAuOXpNMjAuOSA4MS45VjY4aDVjNSAwIDcuNSAyIDcuNSA3IDAgNS0yLjMgNi44LTcuNiA2LjhIMjAuOXoiLz48L3N2Zz4=",
                    Format = "SVG",
                    Description = "BCIT Logo",
                },
                new Image()
                {
                    ThemeName = "Snowdrop",
                    ID = "Video Thumb",
                    Type = "URL",
                    Value = "https://www.bcit.ca/images/entryblocks/video_thumb_sd.png",
                    Format = "URL",
                    Description = "Education For a Complex World",
                },
                new Image()
                {
                    ThemeName = "Maple",
                    ID = "Canadian Flag",
                    Type = "URL",
                    Value = "https://flaglane.com/download/canadian-flag/canadian-flag.svg",
                    Format = "SVG",
                    Description = "Canadian Flag",
                },
                new Image()
                {
                    ThemeName = "Maple",
                    ID = "Government of Canada",
                    Type = "URL",
                    Value = "https://www.canada.ca/etc/designs/canada/wet-boew/assets/sig-blk-en.svg",
                    Format = "SVG",
                    Description = "Government of Canada",
                }
            };
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