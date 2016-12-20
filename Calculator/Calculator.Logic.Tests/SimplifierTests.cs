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
        public void Simplification()
        {
            Check("2-2", "0");
        }

        [Test]
        public void bla()
        {
            Check("2a+(3+2)", "2*a + (5)");
        }
        
    }
}
