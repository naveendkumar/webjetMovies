using System;
using System.Collections.Generic;
using System.Text;
using Webjet.Movies.DataLayer.DataDTO;
using Webjet.Movies.Models;

namespace Webjet.Movies.BusinessLayer.MappingProfile
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<DataLayerMovieCollection, MovieCollection>()
                .ForMember(dest => dest.Title, m => m.MapFrom(source => source.Title))
                .ForMember(dest => dest.Poster, m => m.MapFrom(source => source.Poster))
                .ForMember(dest => dest.Id, m => m.MapFrom(source => source.Id))
                .ForMember(dest => dest.Type, m => m.MapFrom(source => source.Type))
                .ForMember(dest => dest.Year, m => m.MapFrom(source => source.Year))
                .ForMember(dest => dest.Source, x => x.Ignore())
                .ForMember(dest => dest.Price, x => x.Ignore());
            CreateMap<DataLayerMovie, Movie>()
                .ForMember(dest => dest.Title, m => m.MapFrom(source => source.Title))
                .ForMember(dest => dest.Year, m => m.MapFrom(source => source.Year))
                .ForMember(dest => dest.Rated, m => m.MapFrom(source => source.Rated))
                .ForMember(dest => dest.Released, m => m.MapFrom(source => source.Released))
                .ForMember(dest => dest.Runtime, m => m.MapFrom(source => source.Runtime))
                .ForMember(dest => dest.Genre, m => m.MapFrom(source => source.Genre))
                .ForMember(dest => dest.Director, m => m.MapFrom(source => source.Director))
                .ForMember(dest => dest.Writer, m => m.MapFrom(source => source.Writer))
                .ForMember(dest => dest.Actors, m => m.MapFrom(source => source.Actors))
                .ForMember(dest => dest.Plot, m => m.MapFrom(source => source.Plot))
                .ForMember(dest => dest.Language, m => m.MapFrom(source => source.Language))
                .ForMember(dest => dest.Country, m => m.MapFrom(source => source.Country))
                .ForMember(dest => dest.Awards, m => m.MapFrom(source => source.Awards))
                .ForMember(dest => dest.Poster, m => m.MapFrom(source => source.Poster))
                .ForMember(dest => dest.Metascore, m => m.MapFrom(source => source.Metascore))
                .ForMember(dest => dest.Rating, m => m.MapFrom(source => source.Rating))
                .ForMember(dest => dest.Votes, m => m.MapFrom(source => source.Votes))
                .ForMember(dest => dest.Id, m => m.MapFrom(source => source.Id))
                .ForMember(dest => dest.Type, m => m.MapFrom(source => source.Type))
                .ForMember(dest => dest.Price, m => m.MapFrom(source => Convert.ToDecimal(source.Price)));


        }
    }
}
