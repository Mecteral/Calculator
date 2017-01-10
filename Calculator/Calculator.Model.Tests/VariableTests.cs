using FluentAssertions;
using NUnit.Framework;
using System;

namespace Calculator.Model.Tests
{
    [TestFixture]
    public class VariableTests
    {
        [Test]
        public void ToString_Is_HumanReadble()
        {
            new Variable() {Variables = "alpha"}.ToString().Should().Be("alpha");
        }
        [Test]
        public void ReplaceChild_Throws()
        {
            var underTest= new Variable();
            Action a = () => underTest.ReplaceChild(new Constant(), new Constant());
            a.ShouldThrow<InvalidOperationException>();
        }
    }
}