using Duende.IdentityServer.Models;

namespace AuthenticationIdentityServer
{
    public static class IdentityConfig
    {
        public static IEnumerable<Client> Clients =>
               new[]
               {
            new Client
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "api1" }
            },

               };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[] { new ApiScope("api1", "My API") };
    }
}