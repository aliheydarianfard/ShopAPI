using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),

            };

        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("SellAPI",new List<string>(){"name","role" }),

            };
        public static IEnumerable<Client> Clients =>
    new List<Client>
    {
                new Client
                {
                    ClientId = "MobileClient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "SellAPI", "openid","profile" }


                },
                 new Client
                            {
                                ClientId = "client",
                                // no interactive user, use the clientid/secret for authentication
                                AllowedGrantTypes = GrantTypes.ClientCredentials,
                                // secret for authentication
                                ClientSecrets =
                                {
                                    new Secret("secret".Sha256())
                                },
                                // scopes that client has access to
                                AllowedScopes = { "SellAPI"},
                                   Claims=new List<Claim>(){ new Claim("role","admin")}

                            },
    };

    }
}
