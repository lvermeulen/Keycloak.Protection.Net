using System.Collections.Generic;
using Newtonsoft.Json;

namespace Keycloak.Protection.Net
{
    public class ResourceRequest
    {
        [JsonProperty("_id")] 
        public string Id { get; set; }
        [JsonProperty("name")] 
        public string Name { get; set; }
        [JsonProperty("description")] 
        public string Description { get; set; }
        [JsonProperty("owner")]
        public string Owner { get; set; }
        [JsonProperty("ownerManagedAccess")]
        public bool? OwnerManagedAccess { get; set; }
        [JsonProperty("type")] 
        public string Type { get; set; }
        [JsonProperty("icon_uri")] 
        public string IconUri { get; set; }
        [JsonProperty("resource_scopes")] 
        public IEnumerable<string> ResourceScopes { get; set; }
    }
}
