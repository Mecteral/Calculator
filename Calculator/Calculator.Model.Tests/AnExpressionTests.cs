using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Model.Tests
{
    [TestFixture]
    public class AnExpressionTests
    {
        class Testable : AnExpression
        {
            public override void Accept(IExpressionVisitor visitor)
            {
                throw new NotImplementedException();
            }
            public override void ReplaceChild(IExpression oldChild, IExpression newChild)
            {
                throw new NotImplementedException();
            }
            public void SetParent()
            {
                Parent = new ParenthesedExpression();
            }
            public override IEnumerable<IExpression> Children { get; } 
        }

        [Test]
        public void HasParent_Is_False_If_Parent_Is_Null()
        {
            new Testable().HasParent.Should().BeFalse();
        }
        [Test]
        public void HasParent_Is_True_If_Parent_IsNot_Null()
        {
            var underTest = new Testable();
            underTest.SetParent();
            underTest.HasParent.Should().BeTrue();
        }
        [Test]
        public void Parent_Is_Null_By_Default()
        {
            new Testable().Parent.Should().BeNull();
        }
    }
}