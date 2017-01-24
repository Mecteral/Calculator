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
    public class DirectCalculationSimplifierTests
    {
        static void Check(string input, string expected)
        {
            var tokens = Tokenize(input);
            var inputTree = CreateInMemoryModel(tokens);
            var underTest = new DirectCalculationSimplifier();
            var simplified = underTest.Simplify(inputTree);
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
        public void Simplification_Of_Constants_in_Parentheses()
        {
            Check("(3)+2a", "(3) + 2*a");
        }
        [Test]
        public void Simplification_Of_Nested_Additions()
        {
            Check("2+2+2+2a", "4 + 2 + 2*a");
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
        public void Simplification_Works_With_Division()
        {
            Check("3/2-4a+4/4/2", "1.5 - 4*a + 1/2");
        }
        [Test]
        public void Simplification_Works_With_Multiplication()
        {
            Check("1*2*3-4a+5*6*7", "2*3 - 4*a + 30*7");
        }
        [Test]
        public void Simplification_Works_With_Parentheses()
        {
            Check("(1+2)*3+4a+5*(6+7)", "(3)*3 + 4*a + 5*(13)");
        }
        [Test]
        public void Simplifier_Works_As_Calculator()
        {
            Check("1+2-3+5*6/6", "3 - 3 + 30/6");
        }
        [Test]
        public void Simplify_Does_Not_Change_Input_Expression_Tree()
        {
            var input = CreateInMemoryModel(Tokenize("2+2+2+2a"));
            var underTest = new DirectCalculationSimplifier();
            underTest.Simplify(input);
            ((Addition) input).Left.Should().BeOfType<Addition>();
        }
    }
}