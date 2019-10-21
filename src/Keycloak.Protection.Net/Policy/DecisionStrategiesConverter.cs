using System;
using System.Collections.Generic;
using System.Linq;
using Keycloak.Protection.Net.Common;

namespace Keycloak.Protection.Net
{
    public class DecisionStrategiesConverter : JsonEnumConverter<DecisionStrategies>
    {
        private static readonly Dictionary<DecisionStrategies, string> s_pairs = new Dictionary<DecisionStrategies, string>
        {
            [DecisionStrategies.Unanimous] = "UNANIMOUS",
            [DecisionStrategies.Affirmative] = "AFFIRMATIVE",
            [DecisionStrategies.Consensus] = "CONSENSUS"
        }; 

        protected override string EntityString { get; } = "decision strategy";

        public override string ConvertToString(DecisionStrategies value) => s_pairs[value];

        public override DecisionStrategies ConvertFromString(string s)
        {
            var pair = s_pairs.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<DecisionStrategies, string>>.Default.Equals(pair) || pair.Value == null)
            {
                throw new ArgumentException($"Unknown {EntityString}: {s}");
            }

            return pair.Key;
        }
    }
}
