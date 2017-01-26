using Calculator.Logic.Conversion;
using Calculator.Logic.Model.ConversionModel;
using Calculator.Logic.Parsing.ConversionTokenizer;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Conversion
{
    [TestFixture]
    public class MetricToImperialConverterTests
    {
        static IConversionExpression ConvertFromString(string input)
        {
            var tokens = new ConversionTokenizer();
            tokens.Tokenize(input, null);
            var model = new ConversionModelBuilder();
            var converter = new MetricToImperialConverter();
            return converter.Convert(model.BuildFrom(tokens.Tokens));
        }

        [Test]
        public void ImperialToMetricLength()
        {
            var result = ConvertFromString("13km");
            result.Should().BeOfType<ImperialLengthExpression>().Which.Value.Should().Be((decimal)42650.918635);
        }
        [Test]
        public void ImperialToMetricVolume()
        {
            var result = ConvertFromString("13l");
            result.Should().BeOfType<ImperialVolumeExpression>().Which.Value.Should().Be((decimal)439.5822938);
        }
        [Test]
        public void ImperialToMetricArea()
        {
            var result = ConvertFromString("13qm");
            result.Should().BeOfType<ImperialAreaExpression>().Which.Value.Should().Be((decimal)139.93083541723);
        }
        [Test]
        public void ImperialToMetricMass()
        {
            var result = ConvertFromString("13t");
            result.Should().BeOfType<ImperialMassExpression>().Which.Value.Should().Be((decimal)28660.09408405);
        }
    }
}
