using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Evaluation;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Evaluation
{
    [TestFixture]
    public class ExpressionCounterTests
    {
        Constant Number (int number)=> new Constant() {Value = number};
        Sinus Sinus (int number) => new Sinus() {Value = number};
        Tangent Tangent (int number) => new Tangent() {Value = number};
        Cosine Cosine (int number) => new Cosine() {Value = number};
        Variable Variabe (string name) => new Variable() {Name = name};
        Power Power (int left, int right) => new Power() {Left = new Constant() {Value = left}, Right = new Constant() {Value = right} };
        Addition Addition (int left, int right) => new Addition() { Left = new Constant() { Value = left }, Right = new Constant() { Value = right } };
        Subtraction Subtraction (int left, int right) => new Subtraction() { Left = new Constant() { Value = left }, Right = new Constant() { Value = right } };
        Multiplication Multiplication(int left, int right) => new Multiplication() { Left = new Constant() { Value = left }, Right = new Constant() { Value = right } };
        Division Division(int left, int right) => new Division() { Left = new Constant() { Value = left }, Right = new Constant() { Value = right } };
        ParenthesedExpression ParenthesedExpression(IExpression expression) => new ParenthesedExpression() {Wrapped = expression};

        [SetUp]
        public void SetUp()
        {
            mUnderTest = new ExpressionCounter();
        }

        ExpressionCounter mUnderTest;

        void Check(IExpression input, int expected)
        {
            mUnderTest.Evaluate(input).Should().Be(expected);
        }
        [Test]
        public void Count_Of_Addition_Is_Correct()
        {
            Check(Addition(13, 17), 3);
        }

        [Test]
        public void Count_Of_Subtraction_Is_Correct()
        {
            Check(Subtraction(13, 17), 3);
        }

        [Test]
        public void Count_Of_Multiplication_Is_Correct()
        {
            Check(Multiplication(13, 17), 3);
        }

        [Test]
        public void Count_Of_Division_Is_Correct()
        {
            Check(Division(13, 17), 3);
        }

        [Test]
        public void Count_Of_Power_Is_Correct()
        {
            Check(Power(13, 17), 3);
        }

        [Test]
        public void Count_Of_Parentheses_Is_Correct()
        {
            Check(ParenthesedExpression(Addition(13, 17)), 4);
        }

        [Test]
        public void Count_Of_Addition_With_Cosine_Is_Correct()
        {
            var input = new Addition() {Left = Cosine(13), Right = Cosine(17)};
            Check(input, 3);
        }
        [Test]
        public void Count_Of_Addition_With_Sinus_Is_Correct()
        {
            var input = new Addition() { Left = Sinus(13), Right = Sinus(17) };
            Check(input, 3);
        }
        [Test]
        public void Count_Of_Addition_With_Tangent_Is_Correct()
        {
            var input = new Addition() { Left = Tangent(13), Right = Tangent(17) };
            Check(input, 3);
        }
        [Test]
        public void Count_Of_Addition_With_Variables_Is_Correct()
        {
            var input = new Addition() { Left = Variabe("a"), Right = Variabe("b") };
            Check(input, 3);
        }

        [Test]
        public void Complex_Case()
        {
            var input = new Addition() {Left = new Multiplication() {Left = Number(13), Right = Cosine(17)}, Right = new Division() {Left = Number(19), Right = Variabe("a")} };
            Check(input, 7);
        }

    }
}
