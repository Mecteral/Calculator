using System.Collections.Generic;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Logic.Utilities;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Utilities
{
    [TestFixture]
    public class ExpressionEqualityCheckerTests
    {
        static void Check(string first, string second, bool expected)
        {
            var tokensFirst = Tokenize(first);
            var inputTreeFirst = CreateInMemoryModel(tokensFirst);
            var tokensSecond = Tokenize(second);
            var inputTreeSecond = CreateInMemoryModel(tokensSecond);
            var result = new ExpressionEqualityChecker();
            result.IsEqual(inputTreeFirst, inputTreeSecond).Should().Be(expected);
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
        public void EqualExpressionsReturnTrue()
        {
            Check("1/2", "1/2", true);
        }
        [Test]
        public void MultiplicationCaseChecker()
        {
            Check("1*2", "1*2", true);
        }
        [Test]
        public void ParenthesesCaseChecker()
        {
            Check("(1-2)", "(1-2)", true);
        }
        [Test]
        public void SubtractionCaseChecker()
        {
            Check("1-2", "1-2", true);
        }
        [Test]
        public void UnequalExpressionsReturnFalse()
        {
            Check("(1+2)", "1-2*3a", false);
        }
        [Test]
        public void VariableCaseChecker()
        {
            Check("1-2a", "1-2a", true);
        }
    }
}