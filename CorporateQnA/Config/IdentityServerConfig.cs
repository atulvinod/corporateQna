using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateQnA.Models;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace CorporateQnA.Config
{
    public static class IdentityServerConfig
    {

        public static void InitializeIdentityServerConfig(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DB");

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services
             .AddIdentityServer()
             .AddDeveloperSigningCredential()
             .AddAspNetIdentity<AppIdentityUser>()
              .AddConfigurationStore(options =>
              {
                  options.ConfigureDbContext = builder =>
                      builder.UseSqlServer(connectionString,
                          sql => sql.MigrationsAssembly(migrationsAssembly));
              })
                .AddOperationalStore(options =>
                {
                    // this adds the operational data from DB (codes, tokens, consents)
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                });
        }

        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in IdentityServerConfig.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in IdentityServerConfig.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in IdentityServerConfig.GetApis())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in IdentityServerConfig.GetApiScopes())
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

            }
        }

        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

        public static IEnumerable<ApiResource> GetApis() => new List<ApiResource>
        {
            new ApiResource("CorporateQnA"),
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<ApiScope> GetApiScopes() => new List<ApiScope>
        {
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client
            {
                ClientId ="angular",
                ClientSecrets = {new Secret("angular_secret".ToSha256())},
                RequirePkce = true,
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret=false,
                AllowedCorsOrigins =
                {
                    "http://localhost:4200",
                    "http://localhost:5000",
                    "http://localhost:5001",
                    "https://localhost:5000",
                    "https://localhost:5001"
                },
                RedirectUris =
                {
                    "http://localhost:4200",
                    "https://localhost:44384/Home/Signin",
                    "https://localhost:5001",
                    "http://localhost:5000",
                },
                PostLogoutRedirectUris =
                {
                    "http://localhost:4200",
                    "https://localhost:5001",
                    "http://localhost:5000",
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "CorporateQnA",
                    IdentityServerConstants.LocalApi.ScopeName
                },
                AllowAccessTokensViaBrowser=true,
                RequireConsent=false,
                AlwaysIncludeUserClaimsInIdToken=true
            }
        };
    }
}
