using Calculator.Logic.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Model
{
    [TestFixture]
    public class FormattingExpressionVisitorTests
    {
        static void Check(IExpression input, string expected)
        {
            new FormattingExpressionVisitor().Format(input).Should().Be(expected);
        }

        [Test]
        public void Format_Addition()
        {
            Check(TestExpressions.Add3To4, "3 + 4");
        }

        [Test]
        public void Format_Constant()
        {
            Check(TestExpressions.Pi, "3.141");
        }

        [Test]
        public void Format_Division()
        {
            Check(TestExpressions.Divide3By4, "3/4");
        }

        [Test]
        public void Format_Multiplication()
        {
            Check(TestExpressions.Multiply3Times4, "3*4");
        }

        [Test]
        public void Format_Parenthesed()
        {
            Check(TestExpressions.Parenthesed3Minus4, "(3 - 4)");
        }

        [Test]
        public void Format_Subtraction()
        {
            Check(TestExpressions.Subtract4From3, "3 - 4");
        }

        [Test]
        public void Nested_Case()
        {
            Check(TestExpressions.Nested, "((1 - 2) + (3/4))");
        }
    }
}