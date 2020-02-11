using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webjet.Movies.Common.Constants;
using Webjet.Movies.DataLayer;
using Webjet.Movies.DataLayer.DataDTO;
using WebjetMovies.PersistenceLayer.Interfaces;
using WebjetMovies.PersistenceLayer.Model;

namespace Webjet.Movies.BusinessLayer.BusinessLogic
{
    public class FilmWorldService: DataMovieService
    {
        private string _apiBaseUrl { get; }
        private IMemoryCache _cache { get; }
        private string cacheKey = Constants.FilmWorld;

        public FilmWorldService(IHttpClientService httpClientService, IMemoryCache cache, IOptions<AppSettings> appSettings) : base(httpClientService)
        {
            _apiBaseUrl = appSettings.Value.APIClientUrl;
            _cache = cache;
        }

        public override async Task<IEnumerable<DataLayerMovieCollection>> Get()
        {
            List<DataLayerMovieCollection> movies;

            try
            {
                movies = (List<DataLayerMovieCollection>)await base.Get();
                _cache.Set(cacheKey, movies);
            }
            catch (System.Exception e)
            {
                movies = _cache.Get<List<DataLayerMovieCollection>>(cacheKey);
            }

            return movies;
        }

        public override async Task<DataLayerMovie> Get(string id)
        {
            DataLayerMovie remoteMovie;
            try
            {
                remoteMovie = await base.Get(id);
                _cache.Set($"{cacheKey}-{id}", remoteMovie);
            }
            catch (System.Exception e)
            {
                remoteMovie = _cache.Get<DataLayerMovie>($"{cacheKey}-{id}");
            }
            return remoteMovie;
        }

        public override string GetMovieDetailsUrl()
        {
            return $"{_apiBaseUrl}/filmworld/movies";
        }

        public override string GetMovieById(string id)
        {
            return $"{_apiBaseUrl}/filmworld/movie/{id}";
        }
    }
}
