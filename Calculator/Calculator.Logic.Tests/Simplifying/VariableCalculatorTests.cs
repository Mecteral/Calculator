using System.Collections.Generic;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Logic.Simplifying;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplifying
{
    [TestFixture]
    public class VariableCalculatorTests
    {
        static void Check(string input, string expected)
        {
            var tokens = Tokenize(input);
            var inputTree = CreateInMemoryModel(tokens);
            var calculator = new VariableCalculator();
            var underTest = calculator.Simplify(inputTree);
            var asString = new FormattingExpressionVisitor().Format(underTest);
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
        public void AdditionOfVariables()
        {
            Check("1a+2a", "3*a");
        }

        [Test]
        public void CalculationInParentheses()
        {
            Check("(1a+2a)", "(3*a)");
        }

        [Test]
        public void CalculatorDoesntCalculateDifferingVariables()
        {
            Check("1a+2b", "1*a + 2*b");
        }

        [Test]
        public void CalculatorFindsVariablesDownTheChain()
        {
            Check("1a+2+3a", "2 + 4*a");
        }

        [Test]
        public void CalculatorFindsVariablesInComplexForm()
        {
            Check("1+2a+3+4a", "1 + 3 + 6*a");
        }

        [Test]
        public void ComplexAdditionToSubtraction()
        {
            Check("1+3a+2-1a", "1 + 2 - -2*a");
        }

        [Test]
        public void ComplexSubtractionOfVariables()
        {
            Check("1-4a-3-1a", "1 - 0 - 3 - 5*a");
        }

        [Test]
        public void ComplexSubtractionToAddition()
        {
            Check("1-1a-2+2a", "1 - 0 - 2 + 1*a");
        }

        [Test]
        public void SimpleAdditionToSubtraction()
        {
            Check("3a+2-1a", "2 - -2*a");
        }

        [Test]
        public void SimpleAdditionToSubtractionWithConstantAtTheEnd()
        {
            Check("1+2a+3-4a-5", "1 + 3 - 2*a - 5");
        }

        [Test]
        public void SimpleAdditionWithConstantAtTheEnd()
        {
            Check("1+2a+3+4a+5", "1 + 3 + 6*a + 5");
        }

        [Test]
        public void SimpleSubtractionOfVariables()
        {
            Check("3a-2-1a", "0 - 2 + 2*a");
        }

        [Test]
        public void SimpleSubtractionToAddition()
        {
            Check("1a-2+2a", "0 - 2 + 3*a");
        }

        [Test]
        public void SimpleSubtractionToAdditionWithConstantAtTheEnd()
        {
            Check("1-2a-3+4a+5", "1 - 0 - 3 + 2*a + 5");
        }

        [Test]
        public void SimpleSubtractionWithConstantAtTheEnd()
        {
            Check("1-2a-3-4a-5", "1 - 0 - 3 - 6*a - 5");
        }

        [Test]
        public void SubtractionOfVariables()
        {
            Check("1a-1a", "0*a");
        }

        [Test]
        public void SimpleMultiplicationWithLeftConstant()
        {
            Check("1*2a", "2*a");
        }
        [Test]
        public void SimpleMultiplicationWithRightConstant()
        {
            Check("2a*1", "2*a");
        }

        [Test]
        public void MultiplicationAfterAddition()
        {
            Check("1+2*3a", "1 + 6*a");
        }

        [Test]
        public void MultiplicationBeforeAddition()
        {
            Check("1*2a+3", "2*a + 3");
        }

        [Test]
        public void DoubleMultiplicationInAddition()
        {
            Check("1*2a+3*4a", "2*a + 12*a");
        }

        [Test]
        public void SimpleLeftHandedDivisonWithVariable()
        {
            Check("26a/13", "2*a");
        }
        [Test]
        public void SimpleRightHandedDivisonWithVariable()
        {
            Check("26/13a", "2*a");
        }

        [Test]
        public void BoundRightHandedDivisonWithVariable()
        {
            Check("1+26/13a", "1 + 2*a");
        }
        [Test]
        public void BoundLeftHandedDivisonWithVariable()
        {
            Check("26a/13+1", "2*a + 1");
        }
        [Test]
        public void Regression_0001()
        {
            Check("x+1", "1*x+1");
        }
    }
}