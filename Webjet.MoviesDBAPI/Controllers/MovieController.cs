using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Webjet.Movies.BusinessLayer.BusinessLogic;
using Webjet.Movies.BusinessLayer.Interfaces;
using Webjet.Movies.Models;
using Webjet.MoviesDBAPI.Model;

namespace Webjet.MoviesDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
    
        private IMovieService _movieService { get; set; }
        private IMapper _mapper { get; set; }

        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        [HttpGet("movies")]
        public async Task<ActionResult<ResponseViewModel<MoviesByTitle>>> Get()
        {
            try
            {
                var movies = await _movieService.GetMovies();
                return new ResponseViewModel<MoviesByTitle>
                {
                    Success = true,
                    Data = movies,
                    Message = ""
                };
            }
            catch (Exception e)
            {
                return new ResponseViewModel<MoviesByTitle>
                {
                    Success = false,
                    Data = null,
                    Message = e.Message
                };
            }
        }

        [HttpGet("movies/{source}/{id}")]
        public async Task<ActionResult<ResponseViewModel<Movie>>> Get(int source, string id)
        {
            try
            {
                var movie = await _movieService.GetMovieDetailsByID(source, id);

                return new ResponseViewModel<Movie>
                {
                    Success = true,
                    Data = movie,
                    Message = ""
                };
            }
            catch (Exception e)
            {
                return new ResponseViewModel<Movie>
                {
                    Success = false,
                    Data = null,
                    Message = e.Message
                };
            }
        }

        [HttpGet("cheapmovies")]
        public async Task<ActionResult<ResponseViewModel<MoviesByTitle>>> GetCheapMovies()
        {
            try
            {
                var movies = await _movieService.GetMovies();
                List<MovieCollection> totalMovies = new List<MovieCollection>();
                foreach (MoviesInThirdPartyAPI movie in movies.Sources.Values.ToList())
                {
                    if (movie.BookMyShowMovieCollection != null)
                        totalMovies.Add(movie.BookMyShowMovieCollection);
                    if (movie.CinemaWorldMovieCollection != null)
                        totalMovies.Add(movie.CinemaWorldMovieCollection);
                    if (movie.FilmWorldMovieCollection != null)
                        totalMovies.Add(movie.FilmWorldMovieCollection);
                    if (movie.VillageMovieCollection != null)
                        totalMovies.Add(movie.VillageMovieCollection);
                }
                var cheapmovies = totalMovies.GroupBy(x => x.Title).Select(g => g.OrderBy(x => x.Price).First()).OrderBy(x => x.Price);
                return new ResponseViewModel<MoviesByTitle>
                {
                    Success = true,
                    Data = movies,
                    Message = ""
                };
            }
            catch (Exception e)
            {
                return new ResponseViewModel<MoviesByTitle>
                {
                    Success = false,
                    Data = null,
                    Message = e.Message
                };
            }
        }

    }
}