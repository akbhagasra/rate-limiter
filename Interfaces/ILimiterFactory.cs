namespace RateLimiter.Interfaces
{
    public interface ILimiterFactory
    {
        ILimiter GetLimiter(string name);
    }
}
