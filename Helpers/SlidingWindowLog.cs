using RateLimiter.Interfaces;
using System.Diagnostics.Metrics;

namespace RateLimiter.Helpers
{
    public class SlidingWindowLog : ILimiter
    {
        private int capacity;
        private long windowSize;
        private Queue<long> logs;
        private readonly object _lock = new object();
        public SlidingWindowLog(int capacity, int windowSizeInSec)
        {
            this.capacity = capacity;
            this.windowSize = (long)windowSizeInSec * 1000;
            logs = new Queue<long>();
        }
        public bool allowRequest()
        {
            lock (_lock)
            {
                updateLogs();
                return logs.Count <= capacity;
            }
        }
        private void updateLogs()
        {
            long currentTime = System.Environment.TickCount;
            logs.Enqueue(currentTime);
            while(logs.Count > 0 && logs.First() < currentTime - windowSize)
            {
                logs.Dequeue();
            }
        }
    }
}
