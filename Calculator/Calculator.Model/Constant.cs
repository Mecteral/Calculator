using System;

namespace Calculator.Model
{
    /// <summary>
    /// Is a Constant Number of type IExpression
    /// </summary>
    public class Constant : AnExpression
    {
        public decimal Value { get; set; }
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
        public override string ToString() => $"{Value}";
        public override void ReplaceChild(IExpression oldChild, IExpression newChild)
        {
            throw new InvalidOperationException();
        }
    }
}