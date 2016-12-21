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
        [Test]
        public void Simplification_Of_Nested_Additions()
        {
            Check("2+2+2+2a", "6 + 2*a");
        }
        [Test]
        public void Simplification_Of_ParenthesedExpression()
        {
            Check("2a+(3+2)", "2*a + 5");
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
            Check("1+2+3+4+5a+6+7+8+9a", "10 + 5*a + 21 + 9*a");
        }

        [Test]
        public void Simplification_Of_Parentheses_Next_To_Variables()
        {
            Check("(1*2/2+3-4)-2a+(1*2/2+3-4a +5*6)", "0 - 2*a + (4 - 4*a + 30)");
        }

        [Test]
        public void Simplification_Works_With_Multiplication()
        {
            Check("1*2*3-4a+5*6*7", "6 - 4*a + 210");
        }
        [Test]
        public void Simplification_Works_With_Division()
        {
            Check("3/2-4a+4/4/2", "1.5 - 4*a + 0.5");
        }

        [Test]
        public void Simplification_Works_With_Parentheses()
        {
            Check("(1+2)*3+4a+5*(6+7)", "9 + 4*a + 65");
        }

        [Test]
        public void Simplification_Of_Expression_Where_Variable_Is_First_Element()
        {
            Check("5a+3+4", "5*a + 7");
        }

        [Test]
        public void Simplification_Of_Constants_in_Parentheses()
        {
            Check("(3)+2a", "3 + 2*a");
        }

        [Test]
        public void Simplifier_Works_As_Calculator()
        {
            Check("1+2-3+5*6/6", "5");
        }
    }
}