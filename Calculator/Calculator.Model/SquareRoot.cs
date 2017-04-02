using System;

namespace Calculator.Model
{
    public class SquareRoot : AnExpressionWithValue
    {
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override void ReplaceChild(IExpression oldChild, IExpression newChild)
        {
            throw new InvalidOperationException();
        }
    }
}