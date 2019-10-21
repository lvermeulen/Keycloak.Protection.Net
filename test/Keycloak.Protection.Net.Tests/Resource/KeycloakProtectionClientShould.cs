using System;
using System.Threading.Tasks;
using Xunit;

namespace Keycloak.Protection.Net.Tests
{
    public partial class KeycloakProtectionClientShould
    {
        [Theory]
        [InlineData("Insurance", "insurance", "a60c2b25-cbf2-4394-9b62-ecd372dc7877")]
        public async Task CreateGetUpdateGetAllDeleteResourceAsync(string realm, string clientId, string clientSecret)
        {
            // get pat
            string pat = await _client.GetPatAsync(realm, new
            {
                grant_type = "client_credentials",
                client_id = clientId,
                client_secret = new Guid(clientSecret)
            });

            // create
            var createRequest = new ResourceRequest
            {
                Name = "Tweedl Social Service",
                Type = "http://www.example.com/rsrcs/socialstream/140-compatible",
                IconUri = "http://www.example.com/icons/sharesocial.png",
                ResourceScopes = new[]
                {
                    "read-public",
                    "post-updates",
                    "read-private",
                    "http://www.example.com/scopes/all"
                }
            };
            var created = await _client.CreateResourceAsync(realm, pat, createRequest);
            Assert.NotNull(created);

            try
            {
                // get
                var response = await _client.GetResourceAsync(realm, pat, created.Id);
                Assert.NotNull(response);
                Assert.Equal(createRequest.Name, response.Name);
                Assert.Equal(createRequest.Type, response.Type);
                Assert.Equal(createRequest.IconUri, response.IconUri);

                // update
                bool updated = await _client.UpdateResourceAsync(realm, pat, created.Id, new ResourceRequest
                {
                    Id = created.Id,
                    Name = "Tweedl Social Service",
                    ResourceScopes = new[]
                    {
                        "read"
                    }
                });
                Assert.True(updated);

                // get
                response = await _client.GetResourceAsync(realm, pat, created.Id);
                Assert.NotNull(response);
                Assert.Equal(created.Id, response.Id);

                // get all
                var resources = await _client.GetResourcesAsync(realm, pat);
                Assert.NotNull(resources);
                Assert.NotEmpty(resources);
            }
            finally
            {
                // delete
                bool deleted = await _client.DeleteResourceAsync(realm, pat, created.Id);
                Assert.True(deleted);
            }
        }
    }
}
