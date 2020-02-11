using System.Threading.Tasks;

namespace WebjetMovies.PersistenceLayer.Interfaces
{
    public interface IHttpClientService
    {
        Task<T> GetData<T>(string apiUrl);
    }
}
