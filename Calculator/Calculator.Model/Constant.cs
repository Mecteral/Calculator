﻿namespace Calculator.Model
{
    /// <summary>
    /// Is a Constant Number of type IExpression
    /// </summary>
    public class Constant : AnExpression
    {
        public double Value { get; set; }
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}