using System;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Model;
using Calculator.Model;
using FluentAssertions;
using NSubstitute.ExceptionExtensions;
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

        [Test]
        public void SquarewithRoot()
        {
            Check(TestExpressions.SquarewithRoot, 81);
        }

        [Test]
        public void OutputSteps()
        {
            var args = new ApplicationArguments {ShowSteps = true};
            new EvaluatingExpressionVisitor().Evaluate(new Addition() { Left = new Constant() {Value = 13},Right = new Constant() {Value = 17} }, args);
            EvaluatingExpressionVisitor.Steps.Should().Contain("13+17\n 30");
        }

        [Test]
        public void Evaluation_Throws_Exception_On_Variable_Encounter()
        {
            var variableExpression = new Variable() {Variables = "a"};
            var visitor = new EvaluatingExpressionVisitor();
            Action a = () => visitor.Evaluate(variableExpression, null);
            a.ShouldThrow<InvalidOperationException>();
        }
    }
}