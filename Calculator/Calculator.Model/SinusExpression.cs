using System;

namespace Calculator.Model
{
    public class SinusExpression : ATrigonometricFunction
    {
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}