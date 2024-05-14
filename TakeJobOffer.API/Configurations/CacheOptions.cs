using Microsoft.Extensions.Caching.Distributed;

namespace TakeJobOffer.API.Configurations
{
    public static class CacheOptions
    {
        public static DistributedCacheEntryOptions SlidingFiveMinuteOption = new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(5)
        };
    }
}
