using Calculator.Logic.Model;
using Calculator.Model;

namespace Calculator.Logic.Tests.Model
{
    public static class TestExpressions
    {
        public static Addition Add3To4
            => new Addition {Left = new Constant {Value = 3}, Right = new Constant {Value = 4}};
        public static Constant Pi => new Constant {Value = 3.141};
        public static Division Divide3By4
            => new Division {Left = new Constant {Value = 3}, Right = new Constant {Value = 4}};
        public static Multiplication Multiply3Times4
            => new Multiplication {Left = new Constant {Value = 3}, Right = new Constant {Value = 4}};
        public static ParenthesedExpression Parenthesed3Minus4
            =>
                new ParenthesedExpression
                {
                    Wrapped = new Subtraction {Left = new Constant {Value = 3}, Right = new Constant {Value = 4}}
                };
        public static Subtraction Subtract4From3
            => new Subtraction {Left = new Constant {Value = 3}, Right = new Constant {Value = 4}};
        public static ParenthesedExpression Nested
        {
            get
            {
                var sub =
                    new Subtraction {Left = new Constant {Value = 1}, Right = new Constant {Value = 2}}.Parenthesize();
                var divide =
                    new Division {Left = new Constant {Value = 3}, Right = new Constant {Value = 4}}.Parenthesize();
                return new ParenthesedExpression {Wrapped = new Addition {Left = sub, Right = divide}};
            }
        }
    }
}