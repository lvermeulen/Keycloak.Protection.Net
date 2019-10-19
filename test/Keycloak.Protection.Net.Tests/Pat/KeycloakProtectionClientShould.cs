using System;
using System.Threading.Tasks;
using Xunit;

namespace Keycloak.Protection.Net.Tests
{
    public partial class KeycloakProtectionClientShould
    {
        [Theory]
        [InlineData("Insurance", "insurance", "270f5bc7-8dcf-430c-a676-3a5e30b1d105")]
        public async Task GetPatAsync(string realm, string clientId, string clientSecret)
        {
            string response = await _client.GetPatAsync(realm, new
            {
                grant_type = "client_credentials",
                client_id = clientId,
                client_secret = new Guid(clientSecret)
            });
            Assert.NotNull(response);
        }
    }
}
