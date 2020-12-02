
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;


namespace PlanYourVoteLibrary2

{
    public partial class Map
    {
        [JsonProperty("routes")]
        public Route[] Routes { get; set; }

        [JsonProperty("waypoints")]
        public Waypoint[] Waypoints { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }

    public partial class Route
    {
        [JsonProperty("geometry")]
        public string Geometry { get; set; }

        [JsonProperty("legs")]
        public Leg[] Legs { get; set; }

        [JsonProperty("weight_name")]
        public string WeightName { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }
    }

    public partial class Leg
    {
        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("steps")]
        public object[] Steps { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }
    }

    public partial class Waypoint
    {
        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public double[] Location { get; set; }
    }

    public partial class Map
    {
        public static Map FromJson(string json) => JsonConvert.DeserializeObject<Map>(json);
    }

    public static class Serialize
    {
        public static string ToJson(this Map self) => JsonConvert.SerializeObject(self);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
