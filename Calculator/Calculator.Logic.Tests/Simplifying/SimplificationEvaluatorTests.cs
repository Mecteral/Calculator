using System.Collections.Generic;
using System.Linq;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Logic.Simplifying;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplification
{
    [TestFixture]
    public class SimplificationEvaluatorTests
    {
        static void CheckCount(string input, int expected)
        {
            var tokens = Tokenize(input);
            var inputTree = CreateInMemoryModel(tokens);
            var underTest = new SimplificationEvaluator();
            underTest.CountExpression(inputTree).Should().Be(expected);
        }
        static IEnumerable<IExpression> CreateIExpressionEnumerableFromStrings(IEnumerable<string> input)
        {
            var result = new List<IExpression>();
            foreach (var str in input)
            {
                var tokens = Tokenize(str);
                var inputTree = CreateInMemoryModel(tokens);
                result.Add(inputTree);
            }
            return result;
        }
        static void CheckEnumerable(IEnumerable<IExpression> input, int expectedPosition)
        {
            var underTest = new SimplificationEvaluator();
            var frozen = input as IExpression[] ?? input.ToArray();
            var expected = frozen.ElementAt(expectedPosition);
            underTest.FindSmallesExpressionInEnumerable(frozen).Should().Be(expected);
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
        public void BoundAdditionCountIsCorrect()
        {
            CheckCount("3+2+1", 5);
        }
        [Test]
        public void BoundAdditionIsBiggerThanBoundMultiplication()
        {
            var underTest = new List<string> {"3+2+1+0", "3*2+1"};
            CheckEnumerable(CreateIExpressionEnumerableFromStrings(underTest), 1);
        }
        [Test]
        public void CountWithVariableIsCorrect()
        {
            CheckCount("1+a", 5);
        }
        [Test]
        public void MixedArithmeticFunctionCountIsCorrect()
        {
            CheckCount("1+2*3-4/5", 9);
        }
        [Test]
        public void ParenthesedExpressionCountIsCorrect()
        {
            CheckCount("(1+2)", 4);
        }
        [Test]
        public void SimpleAdditionCuuntIsCorrect()
        {
            CheckCount("3+2", 3);
        }
        [Test]
        public void SimpleAdditionIsSmallerThanBoundAddition()
        {
            var underTest = new List<string> {"3+2", "3+2+1"};
            CheckEnumerable(CreateIExpressionEnumerableFromStrings(underTest), 0);
        }
    }
}