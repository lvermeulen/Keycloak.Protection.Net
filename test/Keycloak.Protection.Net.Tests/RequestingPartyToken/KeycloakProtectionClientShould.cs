using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Keycloak.Protection.Net.Tests
{
    public partial class KeycloakProtectionClientShould
    {
        [Theory]
        [InlineData("Insurance", "insurance", "a60c2b25-cbf2-4394-9b62-ecd372dc7877")]
        public async Task IntrospectRequestingPartyTokenAsync(string realm, string clientId, string clientSecret)
        {
            // get pat
            string pat = await _client.GetPatAsync(realm, new
            {
                grant_type = "client_credentials",
                client_id = clientId,
                client_secret = new Guid(clientSecret)
            });

            string authorizationHeaderValue = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"))}";
            var response = await _client.IntrospectRequestingPartyTokenAsync(realm, authorizationHeaderValue, pat);
            Assert.NotNull(response);
        }
    }
}
