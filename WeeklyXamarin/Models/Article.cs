using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Prism.Mvvm;

namespace WeeklyXamarin.Models
{
    public partial class Article : BindableBase
    {
        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Url")]
        public Uri Url { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("EditionId")]
        public string EditionId { get; set; }

        [JsonProperty("Author")]
        public string Author { get; set; }

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Category")]
        public string Category { get; set; }

        private bool _isBookmarked;
        public bool IsBookmarked
        {
            get => _isBookmarked;
            set => SetProperty(ref _isBookmarked, value);
        }

        public override string ToString()
        {
            return $"{Title} {Description} {Author} {Category}".ToLower();
        }
    }
}
