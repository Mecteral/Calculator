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
    public class UnitConverterTests
    {
        void Check(string input, bool toMetric, IConversionExpression expected)
        {
            var tokens = new ConversionTokenizer();
            tokens.Tokenize(input);
            var model = new ConversionModelBuilder();
            var converter = new UnitConverter();
            var result = converter.Convert(model.BuildFrom(tokens.Tokens), toMetric);
            result.Should().Be(expected);
        }

        [Test]
        public void SimpleAdditionGetsCalculatedCorrectly()
        {
            var result = new MetricLengthExpression {Value = (decimal) 26.4008 }; 
            Check("21ft+20m", true, result);
        }
    }
}
