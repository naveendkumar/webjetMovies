using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Webjet.Movies.DataLayer.DataDTO;

namespace Webjet.Movies.DataLayer.Interfaces
{
    interface IDataMovieService
    {
        Task<IEnumerable<DataLayerMovieCollection>> Get();
        Task<DataLayerMovie> Get(string id);
    }
}
