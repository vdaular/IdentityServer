/*
 Copyright (c) 2025 Victor Daniel Aular - https://github.com/vdaular/

Copyright (c) 2024 HigginsSoft, Alexander Higgins - https://github.com/alexhiggins732/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

namespace IdentityServer9.Stores;

/// <summary>
/// Caching decorator for ICorsPolicyService
/// </summary>
/// <seealso cref="IdentityServer9.Services.ICorsPolicyService" />
public class CachingCorsPolicyService<T> : ICorsPolicyService
    where T : ICorsPolicyService
{
    /// <summary>
    /// Class to model entries in CORS origin cache.
    /// </summary>
    public class CorsCacheEntry
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public CorsCacheEntry(bool allowed)
        {
            Allowed = allowed;
        }

        /// <summary>
        /// Is origin allowed.
        /// </summary>
        public bool Allowed { get; }
    }

    private readonly IdentityServerOptions Options;
    private readonly ICache<CorsCacheEntry> CorsCache;
    private readonly ICorsPolicyService Inner;
    private readonly ILogger Logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CachingResourceStore{T}"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="inner">The inner.</param>
    /// <param name="corsCache">The CORS origin cache.</param>
    /// <param name="logger">The logger.</param>
    public CachingCorsPolicyService(IdentityServerOptions options,
        T inner,
        ICache<CorsCacheEntry> corsCache,
        ILogger<CachingCorsPolicyService<T>> logger)
    {
        Options = options;
        Inner = inner;
        CorsCache = corsCache;
        Logger = logger;
    }

    /// <summary>
    /// Determines whether origin is allowed.
    /// </summary>
    /// <param name="origin">The origin.</param>
    /// <returns></returns>
    public virtual async Task<bool> IsOriginAllowedAsync(string origin)
    {
        var entry = await CorsCache.GetAsync(origin,
                      Options.Caching.CorsExpiration,
                      async () => new CorsCacheEntry(await Inner.IsOriginAllowedAsync(origin)),
                      Logger);

        return entry.Allowed;
    }
}
