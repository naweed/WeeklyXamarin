﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeeklyXamarin.Models
{    
    public class Edition
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("UpdatedTimeStamp")]
        public DateTime UpdatedTimeStamp { get; set; }

        [JsonProperty("Introduction")]
        public string Introduction { get; set; }

        [JsonProperty("Summary")]
        public string Summary { get; set; }

        [JsonProperty("PublishDate")]
        public DateTime PublishDate { get; set; }

        [JsonProperty("Curators")]
        public string Curators { get; set; }

        [JsonProperty("Articles")]
        public List<Article> Articles { get; set; }

        public string EditionNo
        {
            get => $"Edition {Id}"; 
        }

        public string SummaryDisplay
        {
            get => (Summary == "No Edition Summary"? "The latest edition of Weekly Xamarin is out, sharing the latest goodness of Xamarin. Edition summary to be fixed in the data feed." : Summary);
        }

    }
}
