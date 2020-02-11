using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webjet.Movies.BusinessLayer.Interfaces;
using Webjet.Movies.Common.Enums;
using Webjet.Movies.DataLayer;
using Webjet.Movies.Models;

namespace Webjet.Movies.BusinessLayer.BusinessLogic
{
    public class MovieService : IMovieService
    {
        private CinemaWorldService _cinemaWorldService { get; set; }
        private FilmWorldService _filmWorldService { get; set; }
        private IMapper _mapper { get; set; }


        public MovieService(CinemaWorldService cinemaWorldService, FilmWorldService filmWorldService, IMapper mapper)
        {
            _mapper = mapper;
            _cinemaWorldService = cinemaWorldService;
            _filmWorldService = filmWorldService;

        }
        public async Task<Movie> GetMovieDetailsByID(int source, string id)
        {
            switch (source)
            {
                case (int)MovieColletionSite.CinemaWorld:
                    return await GetMovieDetail(_cinemaWorldService, id);
                case (int)MovieColletionSite.FilmWorld:
                    return await GetMovieDetail(_filmWorldService, id);
            }

            return null;
        }

        public async Task<MoviesByTitle> GetMovies()
        {
            var cinemaWorldMovies = await GetMovieBriefs(_cinemaWorldService);
            var filmWorldMovies = await GetMovieBriefs(_filmWorldService);
            return GetGroupByTitleMovies(cinemaWorldMovies, filmWorldMovies);
        }

        private MoviesByTitle GetGroupByTitleMovies(List<MovieCollection> cinemaWorldMovieBriefs, List<MovieCollection> filmWorldMovieBriefs)
        {
            var mergedMovies = new Dictionary<string, MoviesInThirdPartyAPI>();

            foreach (var movieBrief in cinemaWorldMovieBriefs)
            {
                mergedMovies.Add(movieBrief.Title, new MoviesInThirdPartyAPI
                {
                    CinemaWorldMovieCollection = movieBrief
                });
            }

            foreach (var movieBrief in filmWorldMovieBriefs)
            {
                if (!mergedMovies.ContainsKey(movieBrief.Title))
                {
                    mergedMovies.Add(movieBrief.Title, new MoviesInThirdPartyAPI
                    {
                        FilmWorldMovieCollection = movieBrief
                    });
                }
                else
                {
                    var movieInExistingTitle = mergedMovies[movieBrief.Title];

                    movieInExistingTitle.FilmWorldMovieCollection = movieBrief;
                }

            }

            return new MoviesByTitle
            {
                Sources = mergedMovies
            };
        }

        private async Task<Movie> GetMovieDetail(DataMovieService remoteMovieService, string id)
        {
            return _mapper.Map<Movie>(await remoteMovieService.Get(id));
        }

        private async Task<List<MovieCollection>> GetMovieBriefs(DataMovieService remoteMovieService)
        {
            var movieBriefs = await remoteMovieService.Get();

            if (movieBriefs == null)
                return new List<MovieCollection>();

            var movies = _mapper.Map<List<MovieCollection>>(movieBriefs);

            //read movie price using multi threads
            var tasks = movies.Select(movie => Task.Run(() => PopulatePriceAndSourceForMovieBrief(remoteMovieService, movie))).ToList();

            await Task.WhenAll(tasks);

            return movies;
        }

        private async Task PopulatePriceAndSourceForMovieBrief(DataMovieService remoteMovieService,
            MovieCollection movieBrief)
        {
            var movieDetail = await GetMovieDetail(remoteMovieService, movieBrief.Id);

            if (movieDetail != null)
            {
                movieBrief.Price = movieDetail.Price;
            }

            if (remoteMovieService.GetType() == typeof(CinemaWorldService))
            {
                movieBrief.Source = MovieColletionSite.CinemaWorld;
            }

            if (remoteMovieService.GetType() == typeof(FilmWorldService))
            {
                movieBrief.Source = MovieColletionSite.FilmWorld;
            }
        }


    }
}
