using System;

namespace Calculator.Model
{
    public class Variable : AnExpressionWithName
    {
        public override string Name { get; set; }
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);

        public override void ReplaceChild(IExpression oldChild, IExpression newChild)
        {
            throw new InvalidOperationException();
        }

        public override string ToString() => $"{Name}";
    }
}