using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Keycloak.Protection.Net.Tests
{
    public partial class KeycloakProtectionClientShould
    {
        [Theory]
        [InlineData("Insurance", "insurance", "270f5bc7-8dcf-430c-a676-3a5e30b1d105", "6cb8f9b0-bf6c-49d0-88a5-d5ebbc924c80")]
        public async Task CreateGetAllDeletePermissionTicketAsync(string realm, string clientId, string clientSecret, string userId)
        {
            // get pat
            string pat = await _client.GetPatAsync(realm, new
            {
                grant_type = "client_credentials",
                client_id = clientId,
                client_secret = new Guid(clientSecret)
            });

            // create resource
            var createResourceRequest = new ResourceRequest
            {
                Name = "2Tweedl Social Service",
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
            var createdResource = await _client.CreateResourceAsync(realm, pat, createResourceRequest);
            Assert.NotNull(createdResource);

            try
            {
                // create
                var createTicketRequest = new PermissionTicketRequest
                {
                    Resource = createdResource.Id,
                    Requester = userId,
                    Granted = true,
                    ScopeName = "read-public"
                };
                var created = await _client.CreatePermissionTicketAsync(realm, pat, createTicketRequest);
                Assert.NotNull(created);

                try
                {
                    // get all
                    var permissionTickets = await _client.GetPermissionTicketsAsync(realm, pat);
                    Assert.NotNull(permissionTickets);
                    Assert.NotEmpty(permissionTickets);
                }
                finally
                {
                    // delete
                    bool deleted = await _client.DeletePermissionTicketAsync(realm, pat, created.Id);
                    Assert.True(deleted);
                }
            }
            finally
            {
                // delete resource
                bool deleted = await _client.DeleteResourceAsync(realm, pat, createdResource.Id);
                Assert.True(deleted);
            }
        }
    }
}
