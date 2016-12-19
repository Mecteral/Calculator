using System.Globalization;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class ModelBuilderTests
    {
        static T TestExpecting<T>(params IToken[] input) where T : IExpression
            => Test(input).Should().BeOfType<T>().Subject;

        static IExpression Test(params IToken[] input) => new ModelBuilder().BuildFrom(input);

        static ParenthesesToken Close => new ParenthesesToken(')');
        static ParenthesesToken Open => new ParenthesesToken('(');
        static NumberToken Number(double number) => new NumberToken(number.ToString(CultureInfo.InvariantCulture));
        static OperatorToken Plus => new OperatorToken('+');
        static OperatorToken Minus => new OperatorToken('-');
        static OperatorToken Times => new OperatorToken('*');
        static OperatorToken DividedBy => new OperatorToken('/');
        static VariableToken Variable(char variable) => new VariableToken(variable);

        [Test]
        public void Complex_Case()
        {
            // (1*(2 + 3/(4 - 5))) - (6*(7 + (8 - 9)))
            var input = new IToken[]
            {
                Open, Number(1), Times, Open, Number(2), Plus, Number(3), DividedBy, Open, Number(4), Minus, Number(5),
                Close, Close, Close, Minus, Open, Number(6), Times, Open, Number(7), Plus, Open, Number(8), Minus,
                Number(9), Close, Close, Close
            };
            var exp = Test(input);
            FormattingExpressionVisitor.Format(exp).Should().Be("(1*(2 + 3/(4 - 5))) - (6*(7 + (8 - 9)))");
        }
        [Test]
        public void Complex_Case_With_Two_Parentheses_In_Parentheses()
        {
            // ((1*2)/(3-4))
            var input = new IToken[]
            {
                Open, Open, Number(1), Times, Number(2), Close, DividedBy, Open, Number(3), Minus, Number(4), Close, Close
            };
            var exp = Test(input);
            FormattingExpressionVisitor.Format(exp).Should().Be("((1*2)/(3 - 4))");
        }

        [Test]
        public void Nested_Parentheses_Build_Correctly()
        {
            //((22))
            var outer = TestExpecting<ParenthesedExpression>(Open, Open,
                Number(22), Close, Close);
            outer.Wrapped.Should()
                .BeOfType<ParenthesedExpression>()
                .Subject.Wrapped.Should()
                .BeOfType<Constant>()
                .Subject.Value.Should()
                .Be(22);
        }

        [Test]
        public void Parenthese_Number_Parenthese_Builds_ParenthesedExpression()
        {
            //(22)
            var parenthesed = TestExpecting<ParenthesedExpression>(Open, Number(22),
                Close);
            parenthesed.Wrapped.Should().BeOfType<Constant>().Subject.Value.Should().Be(22);
        }

        [Test]
        public void PointBeforeAdditionOrSubtraction()
        {
            //3+4*5
            var addition = TestExpecting<Addition>(Number(3), Plus, Number(4), Times, Number(5));
            addition.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(3);
            addition.Right.Should()
                .BeOfType<Multiplication>()
                .Subject.Left.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(4);
            addition.Right.Should()
                .BeOfType<Multiplication>()
                .Subject.Right.Should()
                .BeOfType<Constant>()
                .Which.Value.Should()
                .Be(5);
        }

        [Test]
        public void Number_DividedBy_Number_Builds_Division()
        {
            //3/4
            var division = TestExpecting<Division>(Number(3), DividedBy, Number(4));
            division.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(3);
            division.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(4);
        }
        [Test]
        public void Number_Plus_Number_Builds_Addition()
        {
            //3+4
            var addition = TestExpecting<Addition>(Number(3), Plus, Number(4));
            addition.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(3);
            addition.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(4);
        }
        [Test]
        public void Number_Minus_Number_Builds_Subtraction()
        {
            //3-4
            var subtraction = TestExpecting<Subtraction>(Number(3), Minus, Number(4));
            subtraction.Left.Should().BeOfType<Constant>().Which.Value.Should().Be(3);
            subtraction.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(4);
        }

        [Test]
        public void Single_Number_Token_Builds_ConstantNumber()
        {
            //3.131
            var constantNumber = TestExpecting<Constant>(Number(3.131));
            constantNumber.Value.Should().Be(3.131);
        }

        [Test]
        public void Variable_Minus_Number_Builds_Subtraction()
        {
            //a-4
            var subtraction = TestExpecting<Subtraction>(Variable('a'), Minus, Number(4));
            subtraction.Left.Should().BeOfType<Variable>().Which.Variables.Should().Be("a");
            subtraction.Right.Should().BeOfType<Constant>().Which.Value.Should().Be(4);
        }
    }
}