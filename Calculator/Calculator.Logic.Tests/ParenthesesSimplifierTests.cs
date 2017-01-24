using System.Collections.Generic;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class ParenthesesSimplifierTests
    {
        static void Check(string input, string expected)
        {
            var tokens = Tokenize(input);
            var inputTree = CreateInMemoryModel(tokens);
            var directCalculator = new DirectCalculationSimplifier();
            var directSimplified = directCalculator.Simplify(inputTree);
            var underTest = new ParenthesesSimplifier();
            var simplified = underTest.Simplify(directSimplified);
            var asString = new FormattingExpressionVisitor().Format(simplified);
            asString.Should().Be(expected);
        }
        static IEnumerable<IToken> Tokenize(string input)
        {
            var tokenizer = new Tokenizer();
            tokenizer.Tokenize(input, null);
            var tokens = tokenizer.Tokens;
            return tokens;
        }
        static IExpression CreateInMemoryModel(IEnumerable<IToken> tokens) => new ModelBuilder().BuildFrom(tokens);
        [Test]
        public void ParenthesesDeletionBeforeMultiplication()
        {
            Check("(3)*3a", "3*3*a");
        }
        [Test]
        public void ParenthesesSimplifierDoesntRemoveParethesesFromNestedExpressionsWithOperations()
        {
            Check("(3*2a)+2a", "(6*a) + 2*a");
        }
        [Test]
        public void ParenthesesSimplifierRemovesParenthesesFromSingleConstant()
        {
            Check("(3)+2a", "3 + 2*a");
        }
        [Test]
        public void ParenthesesSimplifierWithDivisionAndSubtraction()
        {
            Check("(1/2)/3 - 4a", "0.5/3 - 4*a");
        }
    }
}