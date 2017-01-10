using System;

namespace Calculator.Model
{
    public class Variable : AnExpression
    {
        public string Variables { get; set; }
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
        public override void ReplaceChild(IExpression oldChild, IExpression newChild)
        {
            throw new InvalidOperationException();
        }
        public override string ToString() => $"{Variables}";
    }
}