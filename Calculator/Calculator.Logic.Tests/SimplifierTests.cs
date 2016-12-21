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
            var inputTree = CreateInMemoryModel(tokens);
            var underTest= new Simplifier(inputTree);
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
            var underTest= new Simplifier(input);
            underTest.Simplify();
            ((Addition) input).Left.Should().BeOfType<Addition>();
        }

        [Test]
        public void Simplification_Of_Nested_Operations_On_Both_Sides_Of_Variables()
        {
            Check("1+2+3+4+5a+6+7+8", "10 + 5*a + 21");
        }

        [Test]
        public void Simplification_Of_Parentheses_Next_To_Variables()
        {
            Check("(1*2/2+3-4)-2a+(1*2/2+3-4a)", "(0) - 2*a + (4 - 4*a)");
        }
    }
}