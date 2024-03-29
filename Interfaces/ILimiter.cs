namespace RateLimiter.Interfaces
{
    public interface ILimiter
    {
        bool allowRequest();
    }
}
