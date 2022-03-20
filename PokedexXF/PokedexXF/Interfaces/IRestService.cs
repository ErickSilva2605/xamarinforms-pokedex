using System.Threading.Tasks;

namespace PokedexXF.Interfaces
{
    public interface IRestService
    {
        Task<T> GetResourceAsync<T>(string url);
        Task<T> GetResourceByNameAsync<T>(string apiEndpoint, string name);
    }
}
