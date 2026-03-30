using System.Collections.Concurrent;

namespace AuthSystem.API.Services
{
    public class TokenBlacklistService : ITokenBlacklistService
    {
        //concurrentDictionary is thread safe, so we can use it to store blacklisted tokens in memory without worrying about
        private static readonly ConcurrentDictionary<string, DateTime> _blacklistedTokens = new();
        public void BlacklistToken(string token)
        {
            _blacklistedTokens.TryAdd(token, DateTime.UtcNow);
        }

        public bool IsTokenBlacklisted(string token)
        {
            return _blacklistedTokens.ContainsKey(token);
        }
    }
}
