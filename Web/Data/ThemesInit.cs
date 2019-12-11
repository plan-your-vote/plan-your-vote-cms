using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models;
using Web.Data;

namespace Web
{
    internal static class ThemesInit
    {
        internal const int DefaultElectionId = 1; // Hardcoded

        internal static void Initialize(ApplicationDbContext context) {
           var themes = GetThemes().ToArray();
           context.Themes.AddRange(themes);
           context.SaveChanges();
           var images = GetImages().ToArray();
           context.Images.AddRange(images);
           context.SaveChanges();
           if (context.Themes?.Any() != true)
           {
               themes = GetThemes().ToArray();
               context.Themes.AddRange(themes);
               context.SaveChanges();
           }
           if (context.Images?.Any() != true)
           {
               images = GetImages().ToArray();
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

        // internal static void Initialize(ApplicationDbContext context)
        // {
        //     if (context.Themes?.Any() != true)
        //     {
        //         var themes = GetThemes().ToArray();
        //         context.Themes.AddRange(themes);
        //         context.SaveChanges();
        //     }

        //     if (context.Images?.Any() != true)
        //     {
        //         var images = GetImages().ToArray();
        //         context.Images.AddRange(images);
        //         context.SaveChanges();
        //     }

        //     if (context.SocialMedias?.Any() != true)
        //     {
        //         var socialMedias = GetSocialMedias().ToArray();
        //         context.SocialMedias.AddRange(socialMedias);
        //         context.SaveChanges();
        //     }
        // }

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
                    Value = "https://vancouver.ca/plan-your-vote/img/logo_home.png",
                    Format = "PNG",
                    Description = "Vancouver Votes",
                },
                new Image()
                {
                    ThemeName = "Snowdrop",
                    Placement = "Footer Logo",
                    Type = "URL",
                    Value = "https://vancouver.ca/plan-your-vote/img/cov_logo.png",
                    Format = "PNG",
                    Description = "City of Vancouver",
                },
                new Image()
                {
                    ThemeName = "Maple",
                    Placement = "Footer Logo",
                    Type = "URL",
                    Value = "https://vancouver.ca/plan-your-vote/img/logo_home.png",
                    Format = "PNG",
                    Description = "Vancouver Votes",
                },
                new Image()
                {
                    ThemeName = "Maple",
                    Placement = "Logo",
                    Type = "URL",
                    Value = "https://vancouver.ca/plan-your-vote/img/cov_logo.png",
                    Format = "PNG",
                    Description = "City of Vancouver",
                },
                new Image()
                {
                    ThemeName = "Ocean",
                    Placement = "Footer Logo",
                    Type = "URL",
                    Value = "https://vancouver.ca/plan-your-vote/img/logo_home.png",
                    Format = "PNG",
                    Description = "Vancouver Votes",
                },
                new Image()
                {
                    ThemeName = "Ocean",
                    Placement = "Logo",
                    Type = "URL",
                    Value = "https://vancouver.ca/plan-your-vote/img/cov_logo.png",
                    Format = "PNG",
                    Description = "City of Vancouver",
                },
                                new Image()
                {
                    ThemeName = "Green",
                    Placement = "Footer Logo",
                    Type = "URL",
                    Value = "https://vancouver.ca/plan-your-vote/img/logo_home.png",
                    Format = "PNG",
                    Description = "Vancouver Votes",
                },
                new Image()
                {
                    ThemeName = "Green",
                    Placement = "Logo",
                    Type = "URL",
                    Value = "https://vancouver.ca/plan-your-vote/img/cov_logo.png",
                    Format = "PNG",
                    Description = "City of Vancouver",
                },
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
                new Theme()
                {
                    ThemeName = "Ocean",
                    Selected = false,
                },
                new Theme()
                {
                    ThemeName = "Green",
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