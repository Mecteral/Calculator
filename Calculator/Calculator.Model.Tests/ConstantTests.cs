using System;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Model.Tests
{
    [TestFixture]
    public class ConstantTests
    {
        [Test]
        public void ReplaceChild_Throws()
        {
            var underTest = new Constant();
            Action a = () => underTest.ReplaceChild(new Constant(), new Constant());
            a.ShouldThrow<InvalidOperationException>();
        }
        [Test]
        [Culture("en-US")]
        public void ToString_Is_Human_Readable()
        {
            new Constant {Value = 3.141M}.ToString().Should().Be("3.141");
        }
    }
}