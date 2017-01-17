using System;

namespace Calculator.Model
{
    public class CosineExpression : ATrigonometricFunction
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