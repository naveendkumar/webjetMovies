using Webjet.Movies.Models;

namespace Webjet.Movies.BusinessLayer.BusinessLogic
{
    public class MoviesInThirdPartyAPI
    {
        public MovieCollection CinemaWorldMovieCollection { get; set; }
        public MovieCollection FilmWorldMovieCollection { get; set; }
        public MovieCollection BookMyShowMovieCollection { get; set; }
        public MovieCollection VillageMovieCollection { get; set; }
    }
}
