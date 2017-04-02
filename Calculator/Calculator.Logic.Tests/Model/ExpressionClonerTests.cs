using Calculator.Logic.Model;
using Calculator.Logic.Utilities;
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
            var equalityChecker = new ExpressionEqualityChecker();
            equalityChecker.IsEqual(result, underTest).Should().BeTrue();
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
            var equalityChecker = new ExpressionEqualityChecker();
            equalityChecker.IsEqual(result, underTest).Should().BeTrue();
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
            var equalityChecker = new ExpressionEqualityChecker();
            equalityChecker.IsEqual(result, underTest).Should().BeTrue();
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
            result.ShouldBeEquivalentTo(underTest, cfg => cfg.Excluding(e => e.Children));
        }
        [Test]
        public void ExpressionClonerClonesSimpleAddition()
        {
            var underTest = new Addition {Left = new Constant {Value = 1}, Right = new Constant {Value = 2}};
            var result = ExpressionCloner.Clone(underTest);
            var equalityChecker = new ExpressionEqualityChecker();
            equalityChecker.IsEqual(result, underTest).Should().BeTrue();
        }
        [Test]
        public void ExpressionClonerClonesSimpleAdditionwithCosine()
        {
            var underTest = new Addition
            {
                Left = new Cosine {Value = 1},
                Right = new Cosine {Value = 2}
            };
            var result = ExpressionCloner.Clone(underTest);
            var equalityChecker = new ExpressionEqualityChecker();
            equalityChecker.IsEqual(result, underTest).Should().BeTrue();
        }
        [Test]
        public void ExpressionClonerClonesSimpleAdditionwithSinus()
        {
            var underTest = new Addition
            {
                Left = new Sinus {Value = 1},
                Right = new Sinus {Value = 2}
            };
            var result = ExpressionCloner.Clone(underTest);
            var equalityChecker = new ExpressionEqualityChecker();
            equalityChecker.IsEqual(result, underTest).Should().BeTrue();
        }
        [Test]
        public void ExpressionClonerClonesSimpleAdditionWithSquareAndRoot()
        {
            var underTest = new Addition
            {
                Left = new SquareRoot {Value = 1},
                Right = new Square {Left = new Constant {Value = 13}, Right = new Constant {Value = 2}}
            };
            var result = ExpressionCloner.Clone(underTest);
            var equalityChecker = new ExpressionEqualityChecker();
            equalityChecker.IsEqual(result, underTest).Should().BeTrue();
        }
        [Test]
        public void ExpressionClonerClonesSimpleAdditionwithTangent()
        {
            var underTest = new Addition
            {
                Left = new Tangent {Value = 1},
                Right = new Tangent {Value = 2}
            };
            var result = ExpressionCloner.Clone(underTest);
            var equalityChecker = new ExpressionEqualityChecker();
            equalityChecker.IsEqual(result, underTest).Should().BeTrue();
        }
    }
}