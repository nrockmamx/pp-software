namespace Domain.Repository
{
    public interface IMemoryStore : IDisposable
    {
        string MakeKeyAsync(string data);
        string MakeKeyAsync(int val1, int val2, int val3);
        Task<string> GetAsync(string cacheKey, string path = "default");
        Task SetAsync(string cacheKey, Object value, double expireTime = 1, string path = "default", bool slide = false);
        Task<string> GetNoDateAsync(string cacheKey, string path = "default");
        Task SetNoDateAsync(string cacheKey, Object value, double expireTime = 1, string path = "default");
    }
}