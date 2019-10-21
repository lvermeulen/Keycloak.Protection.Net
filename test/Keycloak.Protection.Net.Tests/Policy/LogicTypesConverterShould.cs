using System;
using Xunit;

namespace Keycloak.Protection.Net.Tests
{
    public class LogicTypesConverterShould
    {
        private readonly LogicTypesConverter _converter = new LogicTypesConverter();

        [Theory]
        [InlineData(LogicTypes.Positive, "POSITIVE")]
        [InlineData(LogicTypes.Negative, "NEGATIVE")]
        public void ConvertToString(LogicTypes value, string expectedValue)
        {
            string result = _converter.ConvertToString(value);
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData("POSITIVE", LogicTypes.Positive)]
        [InlineData("NEGATIVE", LogicTypes.Negative)]
        public void ConvertFromString(string value, LogicTypes expectedValue)
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
