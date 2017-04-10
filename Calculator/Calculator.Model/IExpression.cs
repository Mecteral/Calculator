using System.Collections.Generic;

namespace Calculator.Model
{
    public interface IExpression
    {
        IExpression Parent { get; }
        bool HasParent { get; }
        int TreeDepth { get; }
        void Accept(IExpressionVisitor visitor);
        void ReplaceChild(IExpression oldChild, IExpression newChild);
        IEnumerable<IExpression> Children { get; }
    }
}