using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserReportsApp.Api.Entities;
using UserReportsApp.Shared.Models;

namespace UserReportsApp.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCors(this IServiceCollection services, IConfiguration configuration)
            => services.AddCors(configuration.GetSection("CORS"));

        public static void AddCors(this IServiceCollection services, IConfigurationSection section)
        {
            var corsPolicyPairs = section.Get<IReadOnlyDictionary<string, CorsPolicyOptions>>();

            services.AddCors(options =>
            {
                foreach (var corsPolicyPair in corsPolicyPairs)
                {
                    var corsPolicyOptions = corsPolicyPair.Value;

                    options.AddPolicy(corsPolicyPair.Key, policy =>
                    {
                        if (corsPolicyOptions.Origins != null)
                        {
                            policy.WithOrigins(corsPolicyOptions.Origins);
                        }

                        if (corsPolicyOptions.Headers != null)
                        {
                            policy.WithHeaders(corsPolicyOptions.Headers);
                        }

                        if (corsPolicyOptions.Methods != null)
                        {
                            policy.WithMethods(corsPolicyOptions.Methods);
                        }

                        if (corsPolicyOptions.AllowCredentials)
                        {
                            policy.AllowCredentials();
                        }
                    });
                }
            });
        }

        //public static void AddAutoMapper(this IServiceCollection services, Action<IMapperConfigurationExpression> configureMapper)
        //{
        //    var configuration = new MapperConfiguration(configureMapper);

        //    services.AddScoped(_ => configuration.CreateMapper());
        //}
    }
}
