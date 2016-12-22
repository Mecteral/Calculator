using Calculator.Logic.Model;
using Calculator.Logic.Parsing;
using Calculator.Model;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class StringSimplifierTests
    {
        [Test]
        public void Simplify()
        {
            var simplifier = Substitute.For<ISimplifier>();
            var tokenizer = Substitute.For<ITokenizer>();
            var formatter = Substitute.For<IExpressionFormatter>();
            var modelBuilder = Substitute.For<IModelBuilder>();
            var underTest = new StringSimplifier(simplifier, tokenizer, formatter, modelBuilder);

            var parsed = new Constant();
            var simplified = new Constant();
            modelBuilder.BuildFrom(tokenizer.Tokens).Returns(parsed);
            simplifier.Simplify(parsed).Returns(simplified);
            formatter.Format(simplified).Returns("expected");

            underTest.Simplify("blabla").Should().Be("expected");
            tokenizer.Received().Tokenize("blabla");
        }
    }
}