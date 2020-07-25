using System.Threading;
using System.Threading.Tasks;

namespace Hcqn.Caching
{
    public interface ICache
    {   
        Task<T> Get<T>(string key, CancellationToken token = default) where T : class;       
        Task Refresh(string key, CancellationToken token = default);        
        Task Remove(string key, CancellationToken token = default);    
        Task Set<T>(string key, T value, CacheEntryOptions options, CancellationToken token = default);
    }
}
