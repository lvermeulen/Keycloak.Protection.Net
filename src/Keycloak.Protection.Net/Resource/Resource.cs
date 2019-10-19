using System.Collections.Generic;
using Newtonsoft.Json;

namespace Keycloak.Protection.Net
{
    public class Resource
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("name")] 
        public string Name { get; set; }
        [JsonProperty("description")] 
        public string Description { get; set; }
        [JsonProperty("owner")]
        public Owner Owner { get; set; }
        [JsonProperty("ownerManagedAccess")]
        public bool? OwnerManagedAccess { get; set; }
        [JsonProperty("type")] 
        public string Type { get; set; }
        [JsonProperty("uris")]
        public IEnumerable<string> Uris { get; set; }
        [JsonProperty("icon_uri")] 
        public string IconUri { get; set; }
        [JsonProperty("scopes")] 
        public IEnumerable<Scope> Scopes { get; set; }
        [JsonProperty("resource_scopes")] 
        public IEnumerable<Scope> ResourceScopes { get; set; }
    }
}
