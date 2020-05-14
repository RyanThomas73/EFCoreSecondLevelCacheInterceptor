using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace EFCoreSecondLevelCacheInterceptor
{
    /// <summary>
    /// CacheAllQueries Options
    /// </summary>
    public class CacheAllQueriesOptions
    {
        /// <summary>
        /// Defines the expiration mode of the cache item.
        /// </summary>
        public CacheExpirationMode ExpirationMode { set; get; }

        /// <summary>
        /// The expiration timeout.
        /// </summary>
        public TimeSpan Timeout { set; get; }

        /// <summary>
        /// Enables or disables the `CacheAllQueries` feature.
        /// </summary>
        public bool IsActive { set; get; }
    }

    /// <summary>
    /// Global Cache Settings
    /// </summary>
    public class EFCoreSecondLevelCacheSettings
    {
        /// <summary>
        /// The selected cache provider
        /// </summary>
        public Type CacheProvider { get; set; }

        /// <summary>
        /// Selected caching provider name
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// CacheAllQueries Options
        /// </summary>
        public CacheAllQueriesOptions CacheAllQueriesOptions { get; set; } = new CacheAllQueriesOptions();

        /// <summary>
        /// Should the debug level loggig be disabled?
        /// </summary>
        public bool DisableLogging { set; get; }

        /// <summary>
        /// Should the cache policy tag comment be removed from the command text?
        /// </summary>
        public bool RemoveCachePolicyTagBeforeExecution { get; set; }
    }

    /// <summary>
    /// Defines EFCoreSecondLevel's Options
    /// </summary>
    public class EFCoreSecondLevelCacheOptions
    {
        internal EFCoreSecondLevelCacheSettings Settings { get; } = new EFCoreSecondLevelCacheSettings();

        /// <summary>
        /// Puts the whole system in cache. In this case calling the `Cacheable()` methods won't be necessary.
        /// If you specify the `Cacheable()` method, its setting will override this global setting.
        /// If you want to exclude some queries from this global cache, apply the `NotCacheable()` method to them.
        /// </summary>
        /// <param name="expirationMode">Defines the expiration mode of the cache items globally.</param>
        /// <param name="timeout">The expiration timeout.</param>
        public EFCoreSecondLevelCacheOptions CacheAllQueries(CacheExpirationMode expirationMode, TimeSpan timeout)
        {
            Settings.CacheAllQueriesOptions = new CacheAllQueriesOptions
            {
                ExpirationMode = expirationMode,
                Timeout = timeout,
                IsActive = true
            };
            return this;
        }

        /// <summary>
        /// You can introduce a custom IEFCacheServiceProvider to be used as the CacheProvider.
        /// </summary>
        /// <typeparam name="T">Implements IEFCacheServiceProvider</typeparam>
        public EFCoreSecondLevelCacheOptions UseCustomCacheProvider<T>() where T : IEFCacheServiceProvider
        {
            Settings.CacheProvider = typeof(T);
            return this;
        }

        /// <summary>
        /// You can introduce a custom IEFCacheServiceProvider to be used as the CacheProvider.
        /// If you specify the `Cacheable()` method options, its setting will override this global setting.
        /// </summary>
        /// <param name="expirationMode">Defines the expiration mode of the cache items globally.</param>
        /// <param name="timeout">The expiration timeout.</param>
        /// <typeparam name="T">Implements IEFCacheServiceProvider</typeparam>
        public EFCoreSecondLevelCacheOptions UseCustomCacheProvider<T>(CacheExpirationMode expirationMode, TimeSpan timeout) where T : IEFCacheServiceProvider
        {
            Settings.CacheProvider = typeof(T);
            Settings.CacheAllQueriesOptions = new CacheAllQueriesOptions
            {
                ExpirationMode = expirationMode,
                Timeout = timeout,
                IsActive = true
            };
            return this;
        }

        /// <summary>
        /// Introduces the built-in `EFMemoryCacheServiceProvider` to be used as the CacheProvider.
        /// </summary>
        public EFCoreSecondLevelCacheOptions UseMemoryCacheProvider()
        {
            Settings.CacheProvider = typeof(EFMemoryCacheServiceProvider);
            return this;
        }

        /// <summary>
        /// Introduces the built-in `EFMemoryCacheServiceProvider` to be used as the CacheProvider.
        /// If you specify the `Cacheable()` method options, its setting will override this global setting.
        /// </summary>
        /// <param name="expirationMode">Defines the expiration mode of the cache items globally.</param>
        /// <param name="timeout">The expiration timeout.</param>
        public EFCoreSecondLevelCacheOptions UseMemoryCacheProvider(CacheExpirationMode expirationMode, TimeSpan timeout)
        {
            Settings.CacheProvider = typeof(EFMemoryCacheServiceProvider);
            Settings.CacheAllQueriesOptions = new CacheAllQueriesOptions
            {
                ExpirationMode = expirationMode,
                Timeout = timeout,
                IsActive = true
            };
            return this;
        }

        /// <summary>
        /// Introduces the built-in `CacheManagerCoreProvider` to be used as the CacheProvider.
        /// </summary>
        public EFCoreSecondLevelCacheOptions UseCacheManagerCoreProvider()
        {
            Settings.CacheProvider = typeof(EFCacheManagerCoreProvider);
            return this;
        }

        /// <summary>
        /// Introduces the built-in `CacheManagerCoreProvider` to be used as the CacheProvider.
        /// If you specify the `Cacheable()` method options, its setting will override this global setting.
        /// </summary>
        /// <param name="expirationMode">Defines the expiration mode of the cache items globally.</param>
        /// <param name="timeout">The expiration timeout.</param>
        public EFCoreSecondLevelCacheOptions UseCacheManagerCoreProvider(CacheExpirationMode expirationMode, TimeSpan timeout)
        {
            Settings.CacheProvider = typeof(EFCacheManagerCoreProvider);
            Settings.CacheAllQueriesOptions = new CacheAllQueriesOptions
            {
                ExpirationMode = expirationMode,
                Timeout = timeout,
                IsActive = true
            };
            return this;
        }

        /// <summary>
        /// Introduces the built-in `EasyCachingCoreProvider` to be used as the CacheProvider.
        /// </summary>
        public EFCoreSecondLevelCacheOptions UseEasyCachingCoreProvider(string providerName)
        {
            Settings.CacheProvider = typeof(EFEasyCachingCoreProvider);
            Settings.ProviderName = providerName;
            return this;
        }

        /// <summary>
        /// Introduces the built-in `EasyCachingCoreProvider` to be used as the CacheProvider.
        /// If you specify the `Cacheable()` method options, its setting will override this global setting.
        /// </summary>
        /// <param name="providerName">Selected caching provider name.</param>
        /// <param name="expirationMode">Defines the expiration mode of the cache items globally.</param>
        /// <param name="timeout">The expiration timeout.</param>
        public EFCoreSecondLevelCacheOptions UseEasyCachingCoreProvider(string providerName, CacheExpirationMode expirationMode, TimeSpan timeout)
        {
            Settings.CacheProvider = typeof(EFEasyCachingCoreProvider);
            Settings.ProviderName = providerName;
            Settings.CacheAllQueriesOptions = new CacheAllQueriesOptions
            {
                ExpirationMode = expirationMode,
                Timeout = timeout,
                IsActive = true
            };
            return this;
        }

        /// <summary>
        /// Should the debug level loggig be disabled?
        /// Set it to true for maximum performance.
        /// </summary>
        public EFCoreSecondLevelCacheOptions DisableLogging(bool value = false)
        {
            Settings.DisableLogging = value;
            return this;
        }

        /// <summary>
        /// Should the cache policy tag comment be removed from the command text?
        /// Set it to true for improved performance in query plan cache scenarios.
        /// </summary>
        public EFCoreSecondLevelCacheOptions RemoveCachePolicyTagBeforeExecution(bool value)
        {
            Settings.RemoveCachePolicyTagBeforeExecution = value;
            return this;
        }
    }

    /// <summary>
    /// ServiceCollection Extensions
    /// </summary>
    public static class EFServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the required services of the EFCoreSecondLevelCacheInterceptor.
        /// </summary>
        public static IServiceCollection AddEFSecondLevelCache(
            this IServiceCollection services,
            Action<EFCoreSecondLevelCacheOptions> options)
        {
            services.AddMemoryCache();
            services.TryAddSingleton<IEFDebugLogger, EFDebugLogger>();
            services.TryAddSingleton<IReaderWriterLockProvider, ReaderWriterLockProvider>();
            services.TryAddSingleton<IEFCacheKeyProvider, EFCacheKeyProvider>();
            services.TryAddSingleton<IEFCachePolicyParser, EFCachePolicyParser>();
            services.TryAddSingleton<IEFCacheDependenciesProcessor, EFCacheDependenciesProcessor>();
            services.TryAddSingleton<IMemoryCacheChangeTokenProvider, EFMemoryCacheChangeTokenProvider>();
            services.TryAddSingleton<IDbCommandInterceptorProcessor, DbCommandInterceptorProcessor>();
            services.TryAddSingleton<SecondLevelCacheInterceptor>();

            configOptions(services, options);

            return services;
        }

        private static void configOptions(IServiceCollection services, Action<EFCoreSecondLevelCacheOptions> options)
        {
            var cacheOptions = new EFCoreSecondLevelCacheOptions();
            options.Invoke(cacheOptions);

            if (cacheOptions.Settings.CacheProvider == null)
            {
                services.TryAddSingleton<IEFCacheServiceProvider, EFMemoryCacheServiceProvider>();
            }
            else
            {
                services.TryAddSingleton(typeof(IEFCacheServiceProvider), cacheOptions.Settings.CacheProvider);
            }

            services.TryAddSingleton(Options.Create(cacheOptions.Settings));
        }
    }
}