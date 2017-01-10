using System;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Model.Tests
{
    [TestFixture(typeof(Addition))]
    [TestFixture(typeof(Subtraction))]
    [TestFixture(typeof(Multiplication))]
    [TestFixture(typeof(Division))]
    public class AnArithmeticOperationTests<T> where T : AnArithmeticOperation, new()
    {
        [Test]
        public void Setting_Left_Sets_Parent_Of_Value()
        {
            var underTest = new T {Left = new Constant()};

            underTest.Left.Parent.Should().BeSameAs(underTest);
        }
        [Test]
        public void Setting_Right_Sets_Parent_Of_Value()
        {
            var underTest = new T {Right = new Constant()};

            underTest.Right.Parent.Should().BeSameAs(underTest);
        }
        [Test]
        public void ReplaceChild_Replaces_Left_If_Passed_Left_Value()
        {
            var underTest = new T {Left = new Constant() {Value = 13}, Right = new Constant() {Value = 17}};
            underTest.ReplaceChild(underTest.Left, new Constant(){Value = 19});

            underTest.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(19);
        }
        [Test]
        public void ReplaceChild_Replaces_Right_If_Passed_Right_Value()
        {
            var underTest = new T {Left = new Constant() {Value = 13}, Right = new Constant() {Value = 17}};
            underTest.ReplaceChild(underTest.Right, new Constant(){Value = 19});

            underTest.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(19);
        }
        [Test]
        public void ReplaceChild_Sets_Old_Lefts_Parent_To_Null_If_Left_Is_Being_Replaced
            ()
        {
            var oldLeft = new Constant() { Value = 13 };
            var underTest = new T { Left = oldLeft, Right = new Constant() { Value = 17 } };
            underTest.ReplaceChild(underTest.Left, new Constant() { Value = 19 });

            oldLeft.HasParent.Should().BeFalse();
        }
        [Test]
        public void ReplaceChild_Sets_Old_Rights_Parent_To_Null_If_Right_Is_Being_Replaced
            ()
        {
            var oldRight = new Constant() { Value = 17 };
            var underTest = new T { Left = new Constant() { Value = 13 }, Right = oldRight };
            underTest.ReplaceChild(underTest.Right, new Constant() { Value = 19 });

            oldRight.HasParent.Should().BeFalse();
        }
        [Test]
        public void ReplaceChild_Throws_If_Passed_OldChild_Is_Neither_Left_Nor_Right()
        {
            var underTest = new T { Left = new Constant() { Value = 13 }, Right = new Constant() { Value = 17 } };

            Action a = () => underTest.ReplaceChild(new Constant(), new Constant());

            a.ShouldThrow<ArgumentException>();
        }
    }
}