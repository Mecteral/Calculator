using System.Diagnostics;

namespace Calculator.Model
{
    /// <summary>
    /// IExpression for Multiplication
    /// </summary>
    public class Multiplication : AnArithmeticOperation
    {
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
        public override string ToString() => $"{Left}*{Right}";
    }
}