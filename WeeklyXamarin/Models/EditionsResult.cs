using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeeklyXamarin.Models
{
    public class EditionsResult
    {
        [JsonProperty("Editions")]
        public List<Edition> Editions { get; set; }
    }
}
