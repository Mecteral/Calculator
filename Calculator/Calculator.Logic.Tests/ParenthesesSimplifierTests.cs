using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing;
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
            var directCalculator = new DirectCalculationSimplifier(inputTree);
            var directSimplified = directCalculator.Simplify();
            var underTest = new ParenthesesSimplifier(directSimplified);
            var simplified = underTest.Simplify();
            var asString = new FormattingExpressionVisitor().Format(simplified);
            asString.Should().Be(expected);
        }
        static IEnumerable<IToken> Tokenize(string input)
        {
            var tokenizer = new Tokenizer();
            tokenizer.Tokenize(input);
            var tokens = tokenizer.Tokens;
            return tokens;
        }
        static IExpression CreateInMemoryModel(IEnumerable<IToken> tokens) => new ModelBuilder().BuildFrom(tokens);

        [Test]
        public void ParenthesesSimplifierRemovesParenthesesFromSingleConstant()
        {
            Check("(3)+2a", "3 + 2*a");
        }

        [Test]
        public void ParenthesesSimplifierDoesntRemoveParethesesFromNestedExpressionsWithOperations()
        {
            Check("(3*2a)+2a", "(6*a) + 2*a");
        }
    }
}
