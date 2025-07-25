﻿using Microsoft.Extensions.Caching.Distributed;

namespace Zattini.Infra.Data.UtilityExternal.Interface
{
    public interface ICacheRedisUti
    {
        public Task<string?> GetStringAsyncWrapper(string key, CancellationToken token = default);
        public Task SetStringAsyncWrapper(string key, string value, CancellationToken token = default(CancellationToken));
        public Task SetStringAsyncWrapper(string key, string value, DistributedCacheEntryOptions options, CancellationToken token = default(CancellationToken));
        public void RemoveWrapper(string key);
    }
}
