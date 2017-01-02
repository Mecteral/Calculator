using Calculator.Logic.Model;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Model
{
    [TestFixture]
    public class ExpressionClonerTests
    {
        [Test]
        public void ExpressionClonerClonesAdditionWithLeftAdditionAndRightMultiplication()
        {
            var underTest = new Addition
            {
                Left = new Addition {Left = new Constant {Value = 1}, Right = new Constant {Value = 2}},
                Right = new Multiplication {Left = new Constant {Value = 3}, Right = new Constant {Value = 4}}
            };
            var result = ExpressionCloner.Clone(underTest);
            result.ShouldBeEquivalentTo(underTest);
        }

        [Test]
        public void ExpressionClonerClonesAdditionWithLeftAdditionAndRightMultiplicationWithVariableAtTheEnd()
        {
            var underTest = new Addition
            {
                Left = new Addition {Left = new Constant {Value = 1}, Right = new Constant {Value = 2}},
                Right = new Multiplication {Left = new Constant {Value = 3}, Right = new Variable {Variables = "a"}}
            };
            var result = ExpressionCloner.Clone(underTest);
            result.ShouldBeEquivalentTo(underTest);
        }

        [Test]
        public void ExpressionClonerClonesAdditionWithLeftAdditionChainAndRightMultiplicationWithVariableAtTheEnd()
        {
            var underTest = new Addition
            {
                Left =
                    new Addition
                    {
                        Left = new Addition {Left = new Constant {Value = 1}, Right = new Constant {Value = 2}},
                        Right = new Constant {Value = 3}
                    },
                Right = new Multiplication {Left = new Constant {Value = 4}, Right = new Variable {Variables = "a"}}
            };
            var result = ExpressionCloner.Clone(underTest);
            result.ShouldBeEquivalentTo(underTest);
        }

        [Test]
        public void ExpressionClonerClonesAdditionWithTwoOperations()
        {
            var underTest = new Addition
            {
                Left = new Addition {Left = new Constant {Value = 1}, Right = new Constant {Value = 2}},
                Right = new Addition {Left = new Constant {Value = 3}, Right = new Constant {Value = 4}}
            };
            var result = ExpressionCloner.Clone(underTest);
            result.ShouldBeEquivalentTo(underTest);
        }

        [Test]
        public void ExpressionClonerClonesSimpleAddition()
        {
            var underTest = new Addition {Left = new Constant {Value = 1}, Right = new Constant {Value = 2}};
            var result = ExpressionCloner.Clone(underTest);
            result.ShouldBeEquivalentTo(underTest);
        }
    }
}