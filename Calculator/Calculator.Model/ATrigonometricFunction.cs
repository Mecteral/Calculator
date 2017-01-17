using System;

namespace Calculator.Model
{
    public abstract class ATrigonometricFunction : AnExpressionWithValue
    {
        public override decimal Value { get; set; }
        public override string ToString() => $"{Value}";

        public override void ReplaceChild(IExpression oldChild, IExpression newChild)
        {
            throw new InvalidOperationException();
        }
    }
}