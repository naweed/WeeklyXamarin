using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeeklyXamarin.Models
{
    public class EditionsList
    {
        [JsonProperty("Editions")]
        public List<Edition> Editions { get; set; }
    }

    public class Edition
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("UpdatedTimeStamp")]
        public DateTime UpdatedTimeStamp { get; set; }

        [JsonProperty("Summary")]
        public string Summary { get; set; }

        [JsonProperty("PublishDate")]
        public DateTime PublishDate { get; set; }
    }
}
