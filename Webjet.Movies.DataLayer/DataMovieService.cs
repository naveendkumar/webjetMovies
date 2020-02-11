using System.Collections.Generic;
using System.Threading.Tasks;
using Webjet.Movies.DataLayer.DataDTO;
using Webjet.Movies.DataLayer.DataLogic;
using Webjet.Movies.DataLayer.Interfaces;
using WebjetMovies.PersistenceLayer.Interfaces;

namespace Webjet.Movies.DataLayer
{
    public abstract class DataMovieService : IDataMovieService
    {
        private IHttpClientService _httpClientService { get; }

        public DataMovieService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public virtual async Task<IEnumerable<DataLayerMovieCollection>> Get()
        {
            var apiUrl = GetMovieDetailsUrl();
            var response = await _httpClientService.GetData<DataMovieCollection>(apiUrl);
            return response.Movies;
        }

        public virtual async Task<DataLayerMovie> Get(string id)
        {
            var apiUrl = GetMovieById(id);
            return await _httpClientService.GetData<DataLayerMovie>(apiUrl);
        }

        public abstract string GetMovieDetailsUrl();
        public abstract string GetMovieById(string id);

    }
}
