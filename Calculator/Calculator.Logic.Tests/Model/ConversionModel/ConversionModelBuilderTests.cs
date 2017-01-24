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

namespace Calculator.Logic.Tests.Model.ConversionModel
{
    [TestFixture]
    public class ConversionModelBuilderTests
    {
        static IConversionExpression BuildModel(string input)
        {
            var tokenizer = new ConversionTokenizer();
            tokenizer.Tokenize(input, null);
            var modelBuilder = new ConversionModelBuilder();
            return modelBuilder.BuildFrom(tokenizer.Tokens);
        }

        [Test]
        public void SingleExpressionGetsReturnedCorrectly()
        {
            var underTest = BuildModel("20ft");
            underTest.Parent.Should().Be(null);
            underTest.HasParent.Should().BeFalse();
            underTest.Should().BeOfType<ImperialLengthExpression>().Which.Value.Should().Be(20);
        }

        [Test]
        public void SingleSubtractionBuildsCorrectly()
        {
            var underTest = BuildModel("20ft-20ft");
            underTest.Should()
                .BeOfType<ConversionSubtraction>()
                .Subject.Left.Should()
                .BeOfType<ImperialLengthExpression>()
                .Which.Value.Should()
                .Be(20);
            underTest.Should()
                .BeOfType<ConversionSubtraction>()
                .Subject.Right.Should()
                .BeOfType<ImperialLengthExpression>()
                .Which.Value.Should()
                .Be(20);
        }

        [Test]
        public void ChainedAdditionBuildsCorrectly()
        {
            var underTest = BuildModel("20m/10ft+20ft");
            underTest.Should()
                .BeOfType<ConversionAddition>()
                .Subject.Left.Should()
                .BeOfType<ConversionDivision>()
                .Subject.Left.Should()
                .BeOfType<MetricLengthExpression>().Which.Value.Should().Be(20);
            underTest.Should()
                .BeOfType<ConversionAddition>()
                .Subject.Left.Should()
                .BeOfType<ConversionDivision>()
                .Subject.Right.Should()
                .BeOfType<ImperialLengthExpression>().Which.Value.Should().Be(10);
            underTest.Should()
                .BeOfType<ConversionAddition>()
                .Subject.Right.Should()
                .BeOfType<ImperialLengthExpression>()
                .Which.Value.Should()
                .Be(20);
        }

        [Test]
        public void ComplexCase()
        {
            var underTest = BuildModel("10m+20l+20sft*10g-20qm+20ha+20ft+20floz+20lb");
            underTest.Should()
                .BeOfType<ConversionAddition>()
                .Subject.Right.Should()
                .BeOfType<ImperialMassExpression>()
                .Which.Value.Should()
                .Be(20);
        }
    }
}
