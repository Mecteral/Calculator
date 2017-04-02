using Calculator.Logic.Model;
using Calculator.Model;

namespace Calculator.Logic.Tests.Model
{
    public static class TestExpressions
    {
        public static Addition Add3To4
            => new Addition {Left = new Constant {Value = 3}, Right = new Constant {Value = 4}};
        public static Constant Pi => new Constant {Value = 3.141M};
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
        public static Addition CosineAddition => new Addition {Left = new Cosine{Value = 23}, Right = new Cosine {Value = 17} };
        public static Addition TangentAddition => new Addition {Left = new Tangent{Value = 23}, Right = new Tangent { Value = 17} };
        public static Addition SinusAddition => new Addition {Left = new Sinus{Value = 23}, Right = new Sinus { Value = 17} };
        public static Square SquarewithRoot=> new Square() {Left = new SquareRoot{Value = 9}, Right = new Constant() { Value = 2} };

    }
}