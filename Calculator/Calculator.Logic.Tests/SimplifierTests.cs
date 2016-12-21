using System.Collections.Generic;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class SimplifierTests
    {
        static void Check(string input, string expected)
        {
            var tokens = Tokenize(input);
            var simplified = UseSimplifier(tokens);
            simplified.Simplify().Should().Be(expected);
        }
        static IEnumerable<IToken> Tokenize(string input)
        {
            var tokenizer = new Tokenizer(input);
            tokenizer.Tokenize();
            var tokens = tokenizer.Tokens;
            return tokens;
        }
        static IExpression CreateInMemoryModel(IEnumerable<IToken> tokens) => new ModelBuilder().BuildFrom(tokens);
        static Simplifier UseSimplifier(IEnumerable<IToken> tokens) => Simplify(CreateInMemoryModel(tokens));
        static Simplifier Simplify(IExpression expression) => new Simplifier(expression);
        [Test]
        public void Simplification_Of_Nested_Additions()
        {
            Check("2+2+2+2a", "6 + 2*a");
        }
        [Test]
        public void Simplification_Of_ParenthesedExpression()
        {
            Check("2a+(3+2)", "2*a + (5)");
        }
        [Test]
        public void Simplification_Without_Variables()
        {
            Check("2-2", "0");
        }
        [Test]
        public void Simplify_Does_Not_Change_Input_Expression_Tree()
        {
            var input = CreateInMemoryModel(Tokenize("2+2+2+2a"));
            Simplify(input);
            ((Addition) input).Left.Should().BeOfType<Addition>();
        }
    }
}