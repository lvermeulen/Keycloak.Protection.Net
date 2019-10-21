using System;
using System.Collections.Generic;
using System.Linq;
using Keycloak.Protection.Net.Common;

namespace Keycloak.Protection.Net
{
    public class LogicTypesConverter : JsonEnumConverter<LogicTypes>
    {
        private static readonly Dictionary<LogicTypes, string> s_pairs = new Dictionary<LogicTypes, string>
        {
            [LogicTypes.Positive] = "POSITIVE",
            [LogicTypes.Negative] = "NEGATIVE"
        }; 

        protected override string EntityString { get; } = "logic";

        public override string ConvertToString(LogicTypes value) => s_pairs[value];

        public override LogicTypes ConvertFromString(string s)
        {
            var pair = s_pairs.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<LogicTypes, string>>.Default.Equals(pair) || pair.Value == null)
            {
                throw new ArgumentException($"Unknown {EntityString}: {s}");
            }

            return pair.Key;
        }
    }
}
