using RateLimiter.Interfaces;

namespace RateLimiter.Helpers
{
    public class SlidingWindowCounter : ILimiter
    {
        private int capacity;
        private long windowSize;
        private Queue<long> logs;
        private readonly object _lock = new object();
        public SlidingWindowCounter(int capacity, int windowSizeInSec)
        {
            this.capacity = capacity;
            this.windowSize = (long)windowSizeInSec * 1000;
            logs = new Queue<long>();
        }
        public bool allowRequest()
        {
            lock (_lock)
            {
                long currentTime = System.Environment.TickCount;
                updateLogs(currentTime);
                if(logs.Count < capacity)
                {
                    logs.Enqueue(currentTime);
                    return true;
                }
                return false;
            }
        }
        private void updateLogs(long currentTime)
        {
            while (logs.Count > 0 && logs.First() < currentTime - windowSize)
            {
                logs.Dequeue();
            }
        }
    }
}
