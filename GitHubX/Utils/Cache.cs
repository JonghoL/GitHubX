using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GitHubX.Utils
{
    public class Cache<TKey, TValue>
    {
        private IImmutableDictionary<TKey, TValue> _cache = ImmutableDictionary.Create<TKey, TValue>();

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _cache.TryGetValue(key, out value);
        }

        public void Add(TKey key, TValue newValue)
        {
            while(true)
            {
                var oldCache = _cache;

                // Add the new value to the cache
                var newCache = oldCache.Add(key, newValue);
                if (Interlocked.CompareExchange(ref _cache, newCache, oldCache) == oldCache)
                {
                    // Cache successfully written
                    //return newValue;
                    return;
                }

                // Failed to write the new cache, try again
            }
        }

        public void Clear()
        {
            _cache = _cache.Clear();
        }
    }
}
