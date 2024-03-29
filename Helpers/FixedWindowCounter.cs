using RateLimiter.Interfaces;

namespace RateLimiter.Helpers
{
    public class FixedWindowCounter : ILimiter
    {
        private int capacity, counter, windowSizeInSec;
        private long windowStart;
        private readonly object _lock = new object();

        public FixedWindowCounter(int capacity, int windowSizeInSec)
        {
            this.capacity = capacity;
            this.windowSizeInSec = windowSizeInSec;
            this.counter = 0;
            this.windowStart = System.Environment.TickCount;
        }
        public bool allowRequest()
        {
            lock (_lock)
            {
                updateCounter();
                if (counter >= capacity)
                    return false;

                counter += 1;
                return true;
            }
        }

        private void updateCounter()
        {
            long currentTime = System.Environment.TickCount;
            int diff = (int)(currentTime - windowStart)/1000;

            if(diff > 0)
            {
                counter = 0;
                windowStart += (long)(diff*windowSizeInSec) * 1000;
            }
        }
    }
}
