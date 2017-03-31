using System.Collections.Generic;

namespace Calculator.Model
{
    public abstract class AnExpression : IExpression
    {
        public IExpression Parent { get; protected internal set; }
        public bool HasParent => null != Parent;
        public abstract void Accept(IExpressionVisitor visitor);
        public abstract void ReplaceChild(IExpression oldChild, IExpression newChild);
        public abstract IEnumerable<IExpression> Children { get; }
    }
}