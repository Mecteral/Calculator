using System;

namespace Calculator.Model
{
    public class Tangent : ATrigonometricFunction
    {
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}