using System;
using System.Collections.Generic;
using System.Linq;
using PlanYourVoteLibrary2;
using Web.Data;

namespace Web
{
    internal static class ThemesInit
    {
        internal const int DefaultElectionId = 1; // Hardcoded

        internal static void Initialize(ApplicationDbContext context)
        {
            if (context.Themes?.Any() != true)
            {
                var themes = GetThemes().ToArray();
                context.Themes.AddRange(themes);
                context.SaveChanges();
            }

            if (context.Images?.Any() != true)
            {
                var images = GetImages().ToArray();
                context.Images.AddRange(images);
                context.SaveChanges();
            }

            if (context.SocialMedias?.Any() != true)
            {
                var socialMedias = GetSocialMedias().ToArray();
                context.SocialMedias.AddRange(socialMedias);
                context.SaveChanges();
            }
        }

        private static List<Image> GetImages()
        {
            return new List<Image>()
            {
                new Image()
                {
                    ThemeName = "Default",
                    Placement = "Logo",
                    Type = "URL",
                    Value = "https://vancouver.ca/plan-your-vote/img/logo_home.png",
                    Format = "PNG",
                    Description = "Vancouver Votes",
                },
                new Image()
                {
                    ThemeName = "Default",
                    Placement = "Footer Logo",
                    Type = "URL",
                    Value = "https://vancouver.ca/plan-your-vote/img/cov_logo.png",
                    Format = "PNG",
                    Description = "City of Vancouver",
                },
                new Image()
                {
                    ThemeName = "Snowdrop",
                    Placement = "Logo",
                    Type = "URL",
                    Value = "https://www.bcit.ca/images/bcitlogo_fallback.png",
                    Format = "PNG",
                    Description = "BCIT Logo",
                },
                new Image()
                {
                    ThemeName = "Snowdrop",
                    Placement = "Footer Logo",
                    Type = "URL",
                    Value = "https://www.bcit.ca/images/v4_entrybanners/bcit_home/home_industryexperts2_sml_hd.jpg",
                    Format = "PNG",
                    Description = "Education For a Complex World",
                },
                new Image()
                {
                    ThemeName = "Maple",
                    Placement = "Footer Logo",
                    Type = "URL",
                    Value = "https://www.canada.ca/etc/designs/canada/wet-boew/assets/wmms-blk.svg",
                    Format = "SVG",
                    Description = "Canadian Flag",
                },
                new Image()
                {
                    ThemeName = "Maple",
                    Placement = "Logo",
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

        private static List<SocialMedia> GetSocialMedias()
        {
            return new List<SocialMedia>()
            {
                new SocialMedia()
                {
                    ElectionId = DefaultElectionId,
                    MediaName = "Facebook",
                    Message = "I'm using Plan Your Vote!",
                    Link = "https://www.facebook.com/"
                },
                new SocialMedia()
                {
                    ElectionId = DefaultElectionId,
                    MediaName = "Twitter",
                    Message = "I'm using Plan Your Vote!",
                    Link = "https://twitter.com/"
                },
                new SocialMedia()
                {
                    ElectionId = DefaultElectionId,
                    MediaName = "LinkedIn",
                    Message = "I'm using Plan Your Vote!",
                    Link = "https://ca.linkedin.com/"
                }
            };
        }
    }
}