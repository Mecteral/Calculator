using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class SimplifierTests
    {
        static void Check(string underTest, string expected)
        {
            var token = new Tokenizer(underTest);
            token.Tokenize();
            var simplified = UseSimplifier(token);
            simplified.Simplify().Should().Be(expected);
        }
        static IExpression CreateInMemoryModel(Tokenizer token) => new ModelBuilder().BuildFrom(token.Tokens);
        static Simplifier UseSimplifier(Tokenizer token) => new Simplifier(CreateInMemoryModel(token));

        [Test]
        public void Simplification_Without_Variables()
        {
            Check("2-2", "0");
        }

        [Test]
        public void Simplification_Of_ParenthesedExpression()
        {
            Check("2a+(3+2)", "2*a + (5)");
        }

        [Test]
        public void Simplification_Of_Nested_Additions()
        {
            Check("2+2+2+2a", "6 + 2*a");
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
