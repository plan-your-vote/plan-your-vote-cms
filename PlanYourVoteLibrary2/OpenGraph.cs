using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PlanYourVoteLibrary2

{
    public class OpenGraph
    {
        [Key]
        public int OpenGraphId { get; set; }

        public string Title { get; set; }

        public string URL { get; set; }

        public string Image { get; set; }

        public string Determiner { get; set; }

        public string Locale { get; set; }

        // public List<string> LocalAlternate { get; set; }

        public string SiteName { get; set; }

        public List<OGImage> Images { get; set; }

        public List<OGAudio> Audios { get; set; }

        public List<OGVideo> Videos { get; set; }
    }

    public class OGImage
    {
        [Key]
        public int OGImageId { get; set; }

        public string Content { get; set; }

        public string SecureURL { get; set; }

        public string Type { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Alt { get; set; }

        public int OpenGraphId { get; set; }

        public OpenGraph OpenGraph { get; set; }
    }

    public class OGAudio
    {
        [Key]
        public int OGAudioID { get; set; }

        public string Content { get; set; }

        public string SecureURL { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public int OpenGraphId { get; set; }

        public OpenGraph OpenGraph { get; set; }
    }

    public class OGVideo
    {
        [Key]
        public int OGVideoID { get; set; }

        public string Content { get; set; }

        public string SecureURL { get; set; }

        public string Type { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int OpenGraphId { get; set; }

        public OpenGraph OpenGraph { get; set; }
    }

    /*
     {
      "og": {
        "basic": {
          "title": "The title of your object as it should appear within the graph, e.g., \"Plan Your Vote\"",
          "url": "http://full-URL.com/abc/defg/plan-your-vote",
          "image": "http://full-URL.com/images/myMainImage.png"
        },
        "optional": {
          "determiner": "the",
          "locale": "en_CA",
          "locale:alternate": ["fr_CA", "en_US", "fr_FR", "es_ES"],
          "site_name": "If your object is part of a larger web site,t he name which should be displayed for the overall site. e.g., \"City of Vancouver\" or \"IMDb\".",
          "images": [
            {
              "content": "http://full-URL.com/images/myDummy.png",
              "secure_url": "https://secure-full-URL.com/images/myDummy.png",
              "type": "image/png",
              "width": 1920,
              "height": 1080,
              "alt": "Lorem ipsum dolor sit amet"
            }
          ],
          "audios": [
            {
              "content": "http://example.com/bond/theme.mp3",
              "secure_url": "https://secure.example.com/bond/theme.mp3",
              "type": "audio/mpeg",
              "description": "A one to two sentence description of your object."
            }
          ],
          "videos": [
            {
              "content": "http://example.com/bond/trailer.swf",
              "video:secure_url": "https://secure.example.com/bond/trailer.swf",
              "video:type": "application/x-shockwave-flash",
              "width": 400,
              "height": 300
            }
          ]
        }
      }
    }

     */
}
