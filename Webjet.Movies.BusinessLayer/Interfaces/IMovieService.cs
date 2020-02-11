using System.Threading.Tasks;
using Webjet.Movies.BusinessLayer.BusinessLogic;
using Webjet.Movies.Models;

namespace Webjet.Movies.BusinessLayer.Interfaces
{
    public interface IMovieService
    {
        Task<MoviesByTitle> GetMovies();
        Task<Movie> GetMovieDetailsByID(int source, string id);
    }
}
