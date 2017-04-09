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
    public class ParenthesesCounterTests
    {
        [SetUp]
        public void SetUp()
        {
            mUnderTest = new ParenthesesCounter();
        }

        ParenthesesCounter mUnderTest;

        [Test]
        public void Parentheses_Count_Simple()
        {
            var input = new Addition() {Left = new Constant(), Right = new ParenthesedExpression() {Wrapped = new Constant()} };
            var result = mUnderTest.Evaluate(input);
            result.Should().Be(1);
        }
        [Test]
        public void Parentheses_Count_Three()
        {
            var input = new Addition() { Left = new Constant(), Right = new ParenthesedExpression() { Wrapped = new ParenthesedExpression() {Wrapped = new ParenthesedExpression() {Wrapped = new Constant()} } } };
            var result = mUnderTest.Evaluate(input);
            result.Should().Be(3);
        }
        [Test]
        public void Parentheses_Count_Complex()
        {
            var input = new Addition() { Left = new ParenthesedExpression() {Wrapped = new Addition() {Left = new ParenthesedExpression() {Wrapped = new Constant()}, Right = new ParenthesedExpression() {Wrapped = new Constant()} } }, Right = new ParenthesedExpression() { Wrapped = new ParenthesedExpression() { Wrapped = new ParenthesedExpression() { Wrapped = new Constant() } } } };
            var result = mUnderTest.Evaluate(input);
            result.Should().Be(6);
        }
    }
}
