using IdentityServer4.Models;
using IdentityServer4;

namespace FunyMovieBackend.IdentityServers
{
    public class IdentityConfiguration
    {
        private static string[] allowedScopes =
{
            IdentityServerConstants.StandardScopes.OfflineAccess,
            IdentityServerConstants.StandardScopes.OpenId
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
           new IdentityResource[]
           {
                new IdentityResources.OpenId(), 
           };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { 
                new ApiScope(IdentityServerConstants.StandardScopes.OfflineAccess),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "app",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 3600,
                    ClientSecrets = { new Secret("K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=".Sha256()) },
                    AllowedScopes = allowedScopes,
                }
            };
    }
}
