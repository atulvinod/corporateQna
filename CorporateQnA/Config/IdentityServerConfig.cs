using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace CorporateQnA.Config
{
    public static class IdentityServerConfig
    {
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
            new ApiScope("BookMyShow"),
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
                    "http://localhost:4200"
                },
                RedirectUris =
                {
                    "http://localhost:4200/#/login",
                    "https://localhost:44384/Home/Signin"
                },
                PostLogoutRedirectUris =
                {
                    "http://localhost:4200"
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
