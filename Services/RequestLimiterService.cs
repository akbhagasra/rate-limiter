using Microsoft.AspNetCore.Mvc;
using RateLimiter.Helpers;
using RateLimiter.Interfaces;

namespace RateLimiter.Services
{
    public class RequestLimiterService
    {
        private Dictionary<String, ILimiter> limiters;
        private readonly object _lock = new object();
        private ILimiterFactory limiterFactory;
        public RequestLimiterService(ILimiterFactory limiterFactory)
        {
            limiters = new Dictionary<String, ILimiter>();
            this.limiterFactory = limiterFactory;
        }

        private void checkIp(string ip, string route)
        {
            lock (_lock)
            {
                if (!limiters.ContainsKey(ip))
                {
                    limiters.Add(ip, limiterFactory.GetLimiter(route));
                }
            }
        }

        public bool processRequest(string ip, string route)
        {
            checkIp(ip, route);
            return limiters[ip].allowRequest();
        }
    }
}
