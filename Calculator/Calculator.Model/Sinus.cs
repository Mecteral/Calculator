using System;

namespace Calculator.Model
{
    public class Sinus : ATrigonometricFunction
    {
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}