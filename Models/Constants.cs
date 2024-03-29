namespace RateLimiter.Models
{
    public static class Constants
    {
        // routes
        public static readonly string TokenBucketRoute = "TokenBucketRoute";
        public static readonly string FixedWindowCounterRoute = "FixedWindowCounterRoute";
        public static readonly string SlidingWindowLogRoute = "SlidingWindowLogRoute";
        public static readonly string SlidingWindowCounterRoute = "SlidingWindowCounterRoute";

        // limiter configs -- this is only for simplicity better way is to consume these configs from environment variables
        public static readonly int TB_Capacity = 10;
        public static readonly int TB_RefillRatePerSec = 1;
        public static readonly int FWC_Capacity = 10;
        public static readonly int FWC_WindowSizeInSec = 10;
        public static readonly int SWL_Capacity = 15;
        public static readonly int SWL_WindowSizeInSec = 3;
        public static readonly int SWC_Capacity = 5;
        public static readonly int SWC_WindowSizeInSec = 1;
    }
}
