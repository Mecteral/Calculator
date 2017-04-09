using Calculator.Logic.Simplifying;
using Calculator.Model;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Simplifying
{
    [TestFixture]
    public class PowerSimplifierTests
    {
        [SetUp]
        public void SetUp()
        {
            mUnderTest = new PowerSimplifier();
        }

        PowerSimplifier mUnderTest;

        [Test]
        public void PowerSimplifier_Changes_Multiplication_To_Power_If_Multiplication_Sides_Are_Of_Same_Value()
        {
            var input = new Multiplication {Left = new Constant {Value = 13}, Right = new Constant {Value = 13}};
            var result = mUnderTest.Simplify(input);
            result.Should().BeOfType<Power>().Which.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(13);
            result.Should().BeOfType<Power>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(2);
        }

        [Test]
        public void
            PowerSimplifier_Does_Not_Change_Multiplication_To_Power_If_Multiplication_Sides_Are_Not_Of_Same_Value()
        {
            var input = new Multiplication {Left = new Constant {Value = 17}, Right = new Constant {Value = 13}};
            var result = mUnderTest.Simplify(input);
            result.Should()
                .BeOfType<Multiplication>()
                .Which.Left.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(17);
            result.Should()
                .BeOfType<Multiplication>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(13);
        }

        [Test]
        public void PowerSimplifier_Finds_Underlying_Multiplication_And_Changes_It_To_Power_Where_Possible()
        {
            var input = new Addition
            {
                Left = new Multiplication {Left = new Constant {Value = 17}, Right = new Constant {Value = 13}},
                Right = new Multiplication {Left = new Constant {Value = 13}, Right = new Constant {Value = 13}}
            };
            var result = mUnderTest.Simplify(input);
            result.Should().BeOfType<Addition>().Which.Left.Should().BeOfType<Multiplication>();
            result.Should().BeOfType<Addition>().Which.Right.Should().BeOfType<Power>();
        }
        [Test]
        public void PowerSimplifier_Finds_Underlying_Multiplication_And_Changes_It_To_Power_Where_Possible_Even_Multiple_Multiplications()
        {
            var input = new Addition
            {
                Left = new Multiplication { Left = new Constant { Value = 13 }, Right = new Constant { Value = 13 } },
                Right = new Multiplication { Left = new Constant { Value = 13 }, Right = new Constant { Value = 13 } }
            };
            var result = mUnderTest.Simplify(input);
            result.Should().BeOfType<Addition>().Which.Left.Should().BeOfType<Power>();
            result.Should().BeOfType<Addition>().Which.Right.Should().BeOfType<Power>();
        }

        [Test]
        public void Multiplication_Of_Same_Variables_Is_Replaced_With_Square()
        {
            var input = new Multiplication() {Left = new Variable() {Name = "x"}, Right = new Variable() {Name = "x"} };
            var result = mUnderTest.Simplify(input);
            result.Should().BeOfType<Power>().Which.Left.Should().BeOfType<Variable>().Which.Name.Should().Be("x");
            result.Should().BeOfType<Power>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(2);
        }

        [Test]
        public void Multiplication_Of_Different_Variables_Is_Not_Replaced_With_Square()
        {
            var input = new Multiplication() { Left = new Variable() { Name = "x" }, Right = new Variable() { Name = "y" } };
            var result = mUnderTest.Simplify(input);
            result.Should().BeOfType<Multiplication>().Which.Left.Should().BeOfType<Variable>().Which.Name.Should().Be("x");
            result.Should().BeOfType<Multiplication>().Which.Right.Should().BeOfType<Variable>().Which.Name.Should().Be("y");
        }

        [Test]
        public void Multiplication_With_Square_Left_And_Same_Variable_Increases_Power_Constant()
        {
            var input = new Multiplication() { Left = new Power() { Left = new Variable() {Name = "x"}, Right = new Constant() {Value = 13} }, Right = new Variable() { Name = "x" } };
            var result = mUnderTest.Simplify(input);
            result.Should().BeOfType<Power>().Which.Left.Should().BeOfType<Variable>().Which.Name.Should().Be("x");
            result.Should().BeOfType<Power>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(14);
        }
        [Test]
        public void Multiplication_With_Square_Right_And_Same_Variable_Increases_Power_Constant()
        {
            var input = new Multiplication() { Left = new Variable() { Name = "x" }, Right =  new Power() { Left = new Variable() { Name = "x" }, Right = new Constant() { Value = 13 } } };
            var result = mUnderTest.Simplify(input);
            result.Should().BeOfType<Power>().Which.Left.Should().BeOfType<Variable>().Which.Name.Should().Be("x");
            result.Should().BeOfType<Power>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(14);
        }
        [Test]
        public void Multiplication_With_Square_Right_And_Different_Variable_Doesnt_Increase_Power_Constant()
        {
            var input = new Multiplication() { Left = new Variable() { Name = "y" }, Right = new Power() { Left = new Variable() { Name = "x" }, Right = new Constant() { Value = 13 } } };
            var result = mUnderTest.Simplify(input);
            result.Should()
                .BeOfType<Multiplication>()
                .Which.Left.Should()
                .BeOfType<Variable>()
                .Which.Name.Should()
                .Be("y");
            result.Should()
                .BeOfType<Multiplication>()
                .Which.Right.Should()
                .BeOfType<Power>()
                .Which.Left.Should().BeOfType<Variable>().Which.Name.Should().Be("x");
            result.Should()
                .BeOfType<Multiplication>()
                .Which.Right.Should()
                .BeOfType<Power>()
                .Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(13);
        }

        [Test]
        public void Multiplication_With_Sinus_And_Cosine_Doesnt_Change_To_Power_Even_Though_The_Value_Is_The_Same()
        {
            var input = new Multiplication() {Left = new Sinus() {Value = 0}, Right = new Cosine() {Value = 0} };
            var result = mUnderTest.Simplify(input);
            result.Should().BeOfType<Multiplication>().Which.Left.Should().BeOfType<Sinus>().Which.Value.Should().Be(0);
            result.Should().BeOfType<Multiplication>().Which.Right.Should().BeOfType<Cosine>().Which.Value.Should().Be(0);
        }

        [Test]
        public void Multiplication_With_Cosine_Changes_To_Power_If_The_Value_Is_The_Same()
        {
            var input = new Multiplication() { Left = new Cosine() { Value = 13 }, Right = new Cosine() { Value = 13 } };
            var result = mUnderTest.Simplify(input);
            result.Should().BeOfType<Power>().Which.Left.Should().BeOfType<Cosine>().Which.Value.Should().Be(13);
            result.Should().BeOfType<Power>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(2);
        }
        [Test]
        public void Multiplication_With_Sinus_Changes_To_Power_If_The_Value_Is_The_Same()
        {
            var input = new Multiplication() { Left = new Sinus() { Value = 13 }, Right = new Sinus() { Value = 13 } };
            var result = mUnderTest.Simplify(input);
            result.Should().BeOfType<Power>().Which.Left.Should().BeOfType<Sinus>().Which.Value.Should().Be(13);
            result.Should().BeOfType<Power>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(2);
        }
        [Test]
        public void Multiplication_With_Tangents_Changes_To_Power_If_The_Value_Is_The_Same()
        {
            var input = new Multiplication() { Left = new Tangent() { Value = 13 }, Right = new Tangent() { Value = 13 } };
            var result = mUnderTest.Simplify(input);
            result.Should().BeOfType<Power>().Which.Left.Should().BeOfType<Tangent>().Which.Value.Should().Be(13);
            result.Should().BeOfType<Power>().Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(2);
        }

        [Test]
        public void Multiplication_With_Square_Right_And_Different_ValueExpressions_Doesnt_Increase_Power_Constant()
        {
            var input = new Multiplication() { Left = new Constant() { Value = 13 }, Right = new Power() { Left = new Sinus() { Value= 13 }, Right = new Constant() { Value = 13 } } };
            var result = mUnderTest.Simplify(input);
            result.Should()
                .BeOfType<Multiplication>()
                .Which.Left.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(13);
            result.Should()
                .BeOfType<Multiplication>()
                .Which.Right.Should()
                .BeOfType<Power>()
                .Which.Left.Should().BeOfType<Sinus>().Which.Value.Should().Be(13);
            result.Should()
                .BeOfType<Multiplication>()
                .Which.Right.Should()
                .BeOfType<Power>()
                .Which.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(13);
        }

        [Test]
        public void Multiplication_With_Square_Right_And_Same_ValueExpressions_Does_Increase_Power_Constant()
        {
            var input = new Multiplication() { Left = new Constant() { Value = 13 }, Right = new Power() { Left = new Constant() { Value = 13 }, Right = new Constant() { Value = 13 } } };
            var result = mUnderTest.Simplify(input);
            result.Should()
                .BeOfType<Power>()
                .Which.Left.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(13);
            result.Should()
                .BeOfType<Power>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should().Be(14);
        }
        [Test]
        public void Multiplication_With_Square_Right_And_Same_SinusValues_Does_Increase_Power_Constant()
        {
            var input = new Multiplication() { Left = new Sinus() { Value = 13 }, Right = new Power() { Left = new Sinus() { Value = 13 }, Right = new Constant() { Value = 13 } } };
            var result = mUnderTest.Simplify(input);
            result.Should()
                .BeOfType<Power>()
                .Which.Left.Should()
                .BeOfType<Sinus>()
                .Which.Value.Should()
                .Be(13);
            result.Should()
                .BeOfType<Power>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should().Be(14);
        }
        [Test]
        public void Multiplication_With_Square_Right_And_Same_TangentValues_Does_Increase_Power_Constant()
        {
            var input = new Multiplication() { Left = new Tangent() { Value = 13 }, Right = new Power() { Left = new Tangent() { Value = 13 }, Right = new Constant() { Value = 13 } } };
            var result = mUnderTest.Simplify(input);
            result.Should()
                .BeOfType<Power>()
                .Which.Left.Should()
                .BeOfType<Tangent>()
                .Which.Value.Should()
                .Be(13);
            result.Should()
                .BeOfType<Power>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should().Be(14);
        }
        [Test]
        public void Multiplication_With_Square_Right_And_Same_CosineValues_Does_Increase_Power_Constant()
        {
            var input = new Multiplication() { Left = new Cosine() { Value = 13 }, Right = new Power() { Left = new Cosine() { Value = 13 }, Right = new Constant() { Value = 13 } } };
            var result = mUnderTest.Simplify(input);
            result.Should()
                .BeOfType<Power>()
                .Which.Left.Should()
                .BeOfType<Cosine>()
                .Which.Value.Should()
                .Be(13);
            result.Should()
                .BeOfType<Power>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should().Be(14);
        }
        [Test]
        public void Multiplication_With_Square_Left_And_Same_CosineValues_Does_Increase_Power_Constant()
        {
            var input = new Multiplication() { Left = new Power() { Left = new Cosine() { Value = 13 }, Right = new Constant() { Value = 13 } }, Right = new Cosine() { Value = 13 } };
            var result = mUnderTest.Simplify(input);
            result.Should()
                .BeOfType<Power>()
                .Which.Left.Should()
                .BeOfType<Cosine>()
                .Which.Value.Should()
                .Be(13);
            result.Should()
                .BeOfType<Power>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should().Be(14);
        }

        [Test]
        public void Multiplication_With_Square_Left_And_Same_TangentValues_Does_Increase_Power_Constant()
        {
            var input = new Multiplication() { Left = new Power() { Left = new Tangent() { Value = 13 }, Right = new Constant() { Value = 13 } }, Right = new Tangent() { Value = 13 } };
            var result = mUnderTest.Simplify(input);
            result.Should()
                .BeOfType<Power>()
                .Which.Left.Should()
                .BeOfType<Tangent>()
                .Which.Value.Should()
                .Be(13);
            result.Should()
                .BeOfType<Power>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should().Be(14);
        }

        [Test]
        public void Multiplication_With_Square_Left_And_Same_SinusValues_Does_Increase_Power_Constant()
        {
            var input = new Multiplication() { Left = new Power() { Left = new Sinus() { Value = 13 }, Right = new Constant() { Value = 13 } }, Right = new Sinus() { Value = 13 } };
            var result = mUnderTest.Simplify(input);
            result.Should()
                .BeOfType<Power>()
                .Which.Left.Should()
                .BeOfType<Sinus>()
                .Which.Value.Should()
                .Be(13);
            result.Should()
                .BeOfType<Power>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should().Be(14);
        }

        [Test]
        public void Multiplication_With_Square_Left_And_Same_ConstantValues_Does_Increase_Power_Constant()
        {
            var input = new Multiplication() { Left = new Power() { Left = new Constant() { Value = 13 }, Right = new Constant() { Value = 13 } }, Right = new Constant() { Value = 13 } };
            var result = mUnderTest.Simplify(input);
            result.Should()
                .BeOfType<Power>()
                .Which.Left.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(13);
            result.Should()
                .BeOfType<Power>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should().Be(14);
        }
        [Test]
        public void Multiplication_With_Square_Left_And_Different_ConstantValues_Doesnt_Increase_Power_Constant()
        {
            var input = new Multiplication() { Left = new Power() { Left = new Constant() { Value = 17 }, Right = new Constant() { Value = 13 } }, Right = new Constant() { Value = 13 } };
            var result = mUnderTest.Simplify(input);
            result.Should()
                .BeOfType<Multiplication>()
                .Which.Left.Should()
                .BeOfType<Power>()
                .Which.Left.Should().BeOfType<Constant>().Which.Value.Should()
                .Be(17);
            result.Should()
                .BeOfType<Multiplication>()
                .Which.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should().Be(13);
        }
    }
}