using RateLimiter.Interfaces;
using RateLimiter.Models;

namespace RateLimiter.Helpers
{
    public class LimiterFactory : ILimiterFactory
    {
        public ILimiter GetLimiter(string name)
        {
            switch (name)
            {
                case "TokenBucketRoute":
                    return new TokenBucket(10, 1);
                case "FixedWindowCounter":
                    return new FixedWindowCounter(10, 10);
                case "SlidingWindowLog":
                    return new SlidingWindowLog(15, 3);
                case "SlidingWindowCounter":
                    return new SlidingWindowCounter(5, 1);
            }
            return new SlidingWindowCounter(5, 1);
        }
    }
}
