using Calculator.Logic.Model;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Model
{
    [TestFixture]
    public class EvaluatingExpressionVisitorTests
    {
        static void Check(IExpression input, decimal expected)
        {
            new EvaluatingExpressionVisitor().Evaluate(input, null).Should().Be(expected);
        }
        [Test]
        public void Evaluate_Addition()
        {
            Check(TestExpressions.Add3To4, 7);
        }
        [Test]
        public void Evaluate_Constant()
        {
            Check(TestExpressions.Pi, 3.141M);
        }
        [Test]
        public void Evaluate_Division()
        {
            Check(TestExpressions.Divide3By4, 0.75M);
        }
        [Test]
        public void Evaluate_Multiplication()
        {
            Check(TestExpressions.Multiply3Times4, 12);
        }
        [Test]
        public void Evaluate_Parenthesed()
        {
            Check(TestExpressions.Parenthesed3Minus4, -1);
        }
        [Test]
        public void Evaluate_Subtraction()
        {
            Check(TestExpressions.Subtract4From3, -1);
        }
        [Test]
        public void Nested_Case()
        {
            Check(TestExpressions.Nested, -0.25M);
        }

        [Test]
        public void Cosine()
        {
            Check(TestExpressions.CosineAddition, 40);
        }
        [Test]
        public void Tangent()
        {
            Check(TestExpressions.TangentAddition, 40);
        }
        [Test]
        public void Sinus()
        {
            Check(TestExpressions.SinusAddition, 40);
        }
    }
}