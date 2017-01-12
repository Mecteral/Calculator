using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Model.ConversionModel;
using Calculator.Logic.Parsing.ConversionTokenizer;
using FluentAssertions;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class ImperialToMetricConverterTests
    {
        IConversionExpression ConvertFromString(string input)
        {
            var tokens = new ConversionTokenizer();
            tokens.Tokenize(input);
            var model = new ConversionModelBuilder();
            var converter = new ImperialToMetricConverter();
            return converter.Convert(model.BuildFrom(tokens.Tokens));
        }

        [Test]
        public void ImperialToMetricLength()
        {
            var result = ConvertFromString("13ft");
            result.Should().BeOfType<MetricLengthExpression>().Which.Value.Should().Be((decimal) 3.9624);
        }
        [Test]
        public void ImperialToMetricVolume()
        {
            var result = ConvertFromString("13gal");
            result.Should().BeOfType<MetricVolumeExpression>().Which.Value.Should().Be((decimal)59.09917);
        }
        [Test]
        public void ImperialToMetricArea()
        {
            var result = ConvertFromString("13sft");
            result.Should().BeOfType<MetricAreaExpression>().Which.Value.Should().Be((decimal)1.2077);
        }
        [Test]
        public void ImperialToMetricMass()
        {
            var result = ConvertFromString("13oz");
            result.Should().BeOfType<MetricMassExpression>().Which.Value.Should().Be((decimal)368.543800625);
        }
    }
}
