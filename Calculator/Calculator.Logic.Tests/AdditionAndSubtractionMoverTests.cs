using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class AdditionAndSubtractionMoverTests
    {
        static void Check(string input, string expected)
        {
            var tokens = Tokenize(input);
            var inputTree = CreateInMemoryModel(tokens);
            var mover = new AdditionAndSubtractionMover();
            var underTest = mover.Simplify(inputTree);
            var asString = new FormattingExpressionVisitor().Format(underTest);
            asString.Should().Be(expected);
        }

        static void CheckWithFullSimplification(string input, string expected)
        {
            var tokens = Tokenize(input);
            var inputTree = CreateInMemoryModel(tokens);
            var simplifier = new Simplifier();
            var underTest = simplifier.Simplify(inputTree);
            var asString  = new FormattingExpressionVisitor().Format(underTest);
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
        public void SimpleMove()
        {
            Check("1+2a+3", "2*a + 1 + 3");
        }

        [Test]
        public void BoundAdditionsTransform()
        {
            Check("a+2+3a+4", "1*a + 3*a + 2 + 4");
        }

        [Test]
        public void ComplexChainGetsTransformedCorrectly()
        {
            Check("a+1+2a+3+4a+5", "1*a + 1 + 2*a + 4*a + 3 + 5");
        }

        [Test]
        public void MoveSubtractionToAddition()
        {
            Check("a-1+2a+3", "1*a + 2*a + 0 - 1 + 3");
        }

        [Test]
        public void SimpleSubtraction()
        {
            Check("a-1-2a-3", "1*a - 2*a - 0 - 1 - 3");
        }

        [Test]
        public void ChainedSubtraction()
        {
            Check("a-1-2a-3-4a-5", "1*a - 1 - 2*a - 4*a - 0 - 3 - 5");
        }

        [Test]
        public void AdditiontoSubtraction()
        {
            Check("a+1-2a-3", "1*a - 2*a + 1 - 3");
        }

        [Test]
        public void CheckWithDivision()
        {
            Check("1/1+a+2+3a-4", "1/1 + 1*a + 3*a + 2 - 4");
        }

        [Test]
        public void CheckWithParenthesed()
        {
            Check("(a-2+3a+4)", "(1*a + 3*a + 0 - 2 + 4)");
        }

        [Test]
        public void ComplexSubtractionCase()
        {
            Check("-a+2-3a+4-5a-6", "0 - 1*a + 2 - 3*a - 5*a + 4 - 6"); 
        }

        [Test]
        public void SimpleMoveWithSubtraction()
        {
            Check("1-2a-3", "2*a - 1 - 3");
        }

        [Test]
        public void SimpleMoveWithAdditionToSubtraction()
        {
            Check("1+2a-3a-4", "2*a - 3*a + 1 - 4");
        }

        [Test]
        public void SimpleMoveWithSubtractionToAddition()
        {
            Check("1-2a+3", "0 - 2*a + 1 + 3");
        }

        [Test]
        public void SimpleMoveSubtractionToSubtraction()
        {
            Check("-1+2a-3+4a-5", "0 - 1 + 2*a + 4*a - 0 - 3 - 5");
        }

        [Test]
        public void SimpleAdditionParenthesed()
        {
            Check("(1+2a+3)", "(2*a + 1 + 3)");
        }

    }
}
