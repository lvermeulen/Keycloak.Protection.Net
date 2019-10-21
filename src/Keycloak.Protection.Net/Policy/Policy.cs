using System.Collections.Generic;
using Newtonsoft.Json;

namespace Keycloak.Protection.Net
{
    public class Policy
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("type")] 
        public string Type { get; set; }
        [JsonProperty("scopes")]
        public IEnumerable<string> Scopes { get; set; }
        [JsonProperty("roles")]
        public IEnumerable<string> Roles { get; set; }
        [JsonProperty("groups")]
        public IEnumerable<string> Groups { get; set; }
        [JsonProperty("clients")]
        public IEnumerable<string> Clients { get; set; }
        [JsonProperty("condition")]
        public string Condition { get; set; }
        [JsonProperty("logic")]
        [JsonConverter(typeof(LogicTypesConverter))]
        public LogicTypes Logic { get; set; }
        [JsonProperty("decisionStrategy")]
        [JsonConverter(typeof(DecisionStrategiesConverter))]
        public DecisionStrategies DecisionStrategy { get; set; }
        [JsonProperty("owner")]
        public string Owner { get; set; }
    }
}
