using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.WebUI.Extensions
{
    public class MemoryCacheTicketStore : ITicketStore
    {
        private const string KeyPrefix = "Ticket";

        private IMemoryCache _memoryCache;

        public MemoryCacheTicketStore()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        public async Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            var key = KeyPrefix + Guid.NewGuid().ToString("N");
            await RenewAsync(key, ticket);
            return key;
        }

        public Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            var options = new MemoryCacheEntryOptions();
            var expiresUtc = ticket.Properties.ExpiresUtc;
            if (expiresUtc.HasValue)
            {
                options.SetAbsoluteExpiration(expiresUtc.Value);
            }
            options.SetSlidingExpiration(TimeSpan.FromHours(1));
            _memoryCache.Set(key, ticket, options);
            return Task.FromResult(0);
        }

        public Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            _memoryCache.TryGetValue(key, out AuthenticationTicket ticket);
            return Task.FromResult(ticket);
        }

        public Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            return Task.FromResult(0);
        }
    }
}
