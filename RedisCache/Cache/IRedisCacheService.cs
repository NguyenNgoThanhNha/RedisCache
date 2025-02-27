namespace RedisCache.Cache;

public interface IRedisCacheService
{
    T? Get<T>(string key);
    void Set<T>(string key, T value);
}