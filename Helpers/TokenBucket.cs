using RateLimiter.Interfaces;

namespace RateLimiter.Helpers
{
    public class TokenBucket : ILimiter
    {
        private int capacity, refillRatePerSec, tokens;
        private long lastUpdated;
        private readonly object _lock = new object();
        public TokenBucket(int capacity, int refillRatePerSec)
        {
            this.capacity = capacity;
            this.refillRatePerSec = refillRatePerSec;
            this.tokens = capacity;
            this.lastUpdated = System.Environment.TickCount;
        }

        public bool allowRequest()
        {
            lock (_lock)
            {
                refill();
                if (tokens == 0)
                    return false;

                tokens -= 1;
                return true;
            }
        }

        private void refill()
        {
            long currentTime = System.Environment.TickCount;
            long duration = currentTime - lastUpdated;
            int tokenToUpdate = (int)(duration / 1000) * refillRatePerSec;
            if (tokenToUpdate > 0)
            {
                tokens = Math.Min(tokenToUpdate + tokens, capacity);
                lastUpdated = currentTime;
            }
        }
    }
}
