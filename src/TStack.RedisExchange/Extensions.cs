using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TStack.RedisExchange.Abstract;
using TStack.RedisExchange.Connection;
using TStack.RedisExchange.Provider;


namespace TStack.RedisExchange
{
    public static class Extensions
    {
        public static IServiceCollection AddTStackRedisCache(this IServiceCollection services, Func<RedisContextConfig> action)
        {
            var config = action();
            var context = new RedisProvider(config, null);

            services.Add(ServiceDescriptor.Singleton<IRedisProvider>(context));

            return services;
        }
    }
}
