using System;
using System.Threading.Tasks;
using Xunit;

namespace Keycloak.Protection.Net.Tests
{
    public partial class KeycloakProtectionClientShould
    {
        [Theory]
        [InlineData("Insurance", "insurance", "a60c2b25-cbf2-4394-9b62-ecd372dc7877")]
        public async Task CreateUpdateGetAllDeletePolicyAsync(string realm, string clientId, string clientSecret)
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
                Name = "Tweedl Social Service",
                Type = "http://www.example.com/rsrcs/socialstream/140-compatible",
                IconUri = "http://www.example.com/icons/sharesocial.png",
                OwnerManagedAccess = true,
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
                var createRequest = new Policy
                {
                    Name = "Access any people manager",
                    Description = "Allow access to any people manager",
                    Scopes = new[] { "read-public" },
                };
                var created = await _client.CreatePolicyAsync(realm, pat, createdResource.Id, createRequest);
                Assert.NotNull(created);

                try
                {
                    // get all
                    var policies = await _client.GetPoliciesAsync(realm, pat);
                    Assert.NotNull(policies);
                    Assert.NotEmpty(policies);

                    // update
                    var updateRequest = new Policy
                    {
                        Id = created.Id,
                        Name = createRequest.Name,
                        Description = createRequest.Description,
                        Type = "uma",
                        Logic = LogicTypes.Positive,
                        DecisionStrategy = DecisionStrategies.Unanimous
                    };
                    bool updated = await _client.UpdatePolicyAsync(realm, pat, created.Id, updateRequest);
                    Assert.True(updated);
                }
                finally
                {
                    // delete
                    bool deleted = await _client.DeletePolicyAsync(realm, pat, created.Id);
                    Assert.True(deleted);
                }
            }
            finally
            {
                // delete resource
                bool deletedResource = await _client.DeleteResourceAsync(realm, pat, createdResource.Id);
                Assert.True(deletedResource);
            }
        }
    }
}
