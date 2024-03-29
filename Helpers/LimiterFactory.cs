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
                    return new TokenBucket(Constants.TB_Capacity, Constants.TB_RefillRatePerSec);
                case "FixedWindowCounter":
                    return new FixedWindowCounter(Constants.FWC_Capacity, Constants.FWC_WindowSizeInSec);
                case "SlidingWindowLog":
                    return new SlidingWindowLog(Constants.SWL_Capacity, Constants.SWL_WindowSizeInSec);
                case "SlidingWindowCounter":
                    return new SlidingWindowCounter(Constants.SWC_Capacity, Constants.SWC_WindowSizeInSec);
            }
            return new SlidingWindowCounter(Constants.SWC_Capacity, Constants.SWC_WindowSizeInSec);
        }
    }
}
