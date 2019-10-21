using System;
using System.Threading.Tasks;
using Xunit;

namespace Keycloak.Protection.Net.Tests
{
    public partial class KeycloakProtectionClientShould
    {
        [Theory]
        [InlineData("Insurance", "insurance", "a60c2b25-cbf2-4394-9b62-ecd372dc7877")]
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
