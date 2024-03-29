using Microsoft.AspNetCore.Mvc;
using RateLimiter.Helpers;
using RateLimiter.Interfaces;

namespace RateLimiter.Services
{
    public class RequestLimiterService
    {
        private Dictionary<String, ILimiter> limiters;
        private readonly object _lock = new object();
        public RequestLimiterService()
        {
            limiters = new Dictionary<String, ILimiter>();
        }

        private void checkIp(string ip)
        {
            lock (_lock)
            {
                if (!limiters.ContainsKey(ip))
                {
                    //limiters.Add(ip, new TokenBucket(10, 1));
                    limiters.Add(ip, new FixedWindowCounter(10, 10));
                }
            }
        }

        public bool processRequest(string ip)
        {
            checkIp(ip);
            return limiters[ip].allowRequest();
        }
    }
}
