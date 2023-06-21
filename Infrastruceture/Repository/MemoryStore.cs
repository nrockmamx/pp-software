using System.Security.Cryptography;
using System.Text;
using Domain.Environments;
using Domain.Repository;
using Microsoft.Extensions.Caching.Distributed;

//using NumberReach.Domain.ServiceBus;

namespace Infrastruceture.Repository
{
     public class MemoryStore : IMemoryStore
    {
        private readonly IDistributedCache _distributedCache;

        public MemoryStore(IDistributedCache distributedCache, IEnvironmentsConfig env)
        {
            _distributedCache = distributedCache;
        }

        public async Task SetAsync(string cacheKey, Object value, double expireTime = 1, string path = "default",bool slide=false)
        {
            cacheKey = ConvertMD5(cacheKey);
            cacheKey = path + ":" + cacheKey;
            DistributedCacheEntryOptions options = null;
            
            if(!slide)
                options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(expireTime));
            else 
                options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(expireTime));
            
            string val = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            
            await _distributedCache.SetStringAsync(cacheKey, val, options);
        }
        
        
        
        public async Task SetNoDateAsync(string cacheKey, Object value, double expireTime = 1, string path = "default")
        {
            //cacheKey = await ConvertMD5(cacheKey);
            cacheKey = path + ":" + cacheKey;
            var options =
                new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(expireTime));

            string val = Newtonsoft.Json.JsonConvert.SerializeObject(value);

            await _distributedCache.SetStringAsync(cacheKey, val, options);
        }

        public async Task<string> GetNoDateAsync(string cacheKey, string path = "default")
        {
            cacheKey = path + ":" + cacheKey;
            var value = await _distributedCache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(value)) return value;

            return null;
        }

        public async Task<string> GetAsync(string cacheKey, string path = "default")
        {
            cacheKey = ConvertMD5(cacheKey);
            cacheKey =  path + ":" + cacheKey;
            var value = await _distributedCache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(value)) return value;

            return null;
        }
        
        public string MakeKeyAsync(string data)
        {
            return ConvertMD5(data);
        }


        public string MakeKeyAsync(int val1, int val2, int val3)
        {
            return $"{val1}_{val2}_{val3}";
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private string ConvertMD5(string key)
        {
            using var hash = MD5.Create();
            var result = string.Join
            (
                "",
                from ba in hash.ComputeHash
                (
                    Encoding.UTF8.GetBytes(key)
                )
                select ba.ToString("x2")
            );

            return result;
        }
    }
}