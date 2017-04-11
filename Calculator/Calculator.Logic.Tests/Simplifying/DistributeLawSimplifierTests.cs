using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Simplifying;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplifying
{
    [TestFixture]
    public class DistributeLawSimplifierTests
    {
        [SetUp]
        public void SetUp()
        {
            mUnderTest = new DistributeLawSimplifier();
        }

        DistributeLawSimplifier mUnderTest;

        [Test]
        public void Simple_Case()
        {
            var input = new Multiplication() {Left = new ParenthesedExpression() {Wrapped = new Addition() {Left = new Constant() {Value = 13}, Right = new Constant() {Value = 17} } }, Right = new Constant() {Value = 2} };
            var result = mUnderTest.Simplify(input);
            result.Should()
                .BeOfType<ParenthesedExpression>()
                .Which.Wrapped.Should()
                .BeOfType<Addition>()
                .Which.Left.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(26);
            result.Should()
                .BeOfType<ParenthesedExpression>()
                .Which.Wrapped.Should()
                .BeOfType<Addition>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(34);
        }
    }
}
