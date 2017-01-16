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
    public class ReadableOutputCreatorTests
    {

        static ConversionTokenizer CreateConversionTokens(string input)
        {
            var token = new ConversionTokenizer();
            token.Tokenize(input);
            return token;
        }
        static void Check(string input, bool toMetric, string expected)
        {
            var token = CreateConversionTokens(input);
            var converted = UseUnitConverter(CreateConversionInMemoryModel(token), toMetric);
            var output = new ReadableOutputCreator();
            var result = output.MakeReadable((IConversionExpressionWithValue) converted);
            result.Should().Be(expected);
        }
        static IConversionExpression CreateConversionInMemoryModel(ConversionTokenizer token) => new ConversionModelBuilder().BuildFrom(token.Tokens);
        static IConversionExpression UseUnitConverter(IConversionExpression expression, bool toMetric) => new UnitConverter().Convert(expression, toMetric);
        [Test]
        public void ImperialMassOutput()
        {
            Check("1it+23lb+234oz+345gr", false," 1 it 2 st 9 lb 10 oz 12 dr 16 gr");
        }
        [Test]
        public void ImperialVolumeOutput()
        {
            Check("1floz + 45335floz", false, " 283 gal 1 qt 3 gi 1 floz");
        }
        [Test]
        public void ImperialAreaOutput()
        {
            Check("1sft + 1acre", false, " 1 acre 1 sft");
        }
        [Test]
        public void ImperialLengthOutput()
        {
            Check("1ft+23454ft", false, " 1 lea 1 mI 3 fur 5 ch 8 yd 1 ft");
        }
        [Test]
        public void MetricMassOutput()
        {
            Check("1t +1kg + 1g+1mg", true, "1.001001001 t");
        }
        [Test]
        public void MetricVolumeOutput()
        {
            Check("1hl+1l+1ml", true, "1.01001 hl");
        }
        [Test]
        public void MetricAreaOutput()
        {
            Check("1qkm+1qm", true, "1.000001 qkm");
        }
        [Test]
        public void MetricLengthOutput()
        {
            Check("1km+1m+1cm", true, "1.00101 km");
        }
    }
}
