using System;
using Xunit;

namespace Keycloak.Protection.Net.Tests
{
    public class DecisionStrategiesConverterShould
    {
        private readonly DecisionStrategiesConverter _converter = new DecisionStrategiesConverter();

        [Theory]
        [InlineData(DecisionStrategies.Unanimous, "UNANIMOUS")]
        [InlineData(DecisionStrategies.Affirmative, "AFFIRMATIVE")]
        [InlineData(DecisionStrategies.Consensus, "CONSENSUS")]
        public void ConvertToString(DecisionStrategies value, string expectedValue)
        {
            string result = _converter.ConvertToString(value);
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData("UNANIMOUS", DecisionStrategies.Unanimous)]
        [InlineData("AFFIRMATIVE", DecisionStrategies.Affirmative)]
        [InlineData("CONSENSUS", DecisionStrategies.Consensus)]
        public void ConvertFromString(string value, DecisionStrategies expectedValue)
        {
            var result = _converter.ConvertFromString(value);
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NotConvertFromString(string value)
        {
            Assert.Throws<ArgumentException>(() => _converter.ConvertFromString(value));
        }
    }
}
