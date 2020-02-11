using Webjet.Movies.Common.Enums;

namespace Webjet.Movies.Models
{
    public class MovieCollection
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
        public decimal Price { get; set; }
        public MovieColletionSite? Source { get; set; }
    }
}
