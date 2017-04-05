using System;
using System.Collections.Generic;

namespace Calculator.Model
{
    /// <summary>
    /// Parenthesed Expression contains Wrapped IExpression
    /// </summary>
    public class ParenthesedExpression : AnExpression
    {
        IExpression mWrapped;
        public IExpression Wrapped
        {
            get { return mWrapped; }
            set
            {
                mWrapped = value;
                mWrapped.Parent(this);
            }
        }
        public override IEnumerable<IExpression> Children => new[] {mWrapped};
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
        public override string ToString() => $"({Wrapped})";
        public override void ReplaceChild(IExpression oldChild, IExpression newChild)
        {
            if (ReferenceEquals(oldChild, mWrapped))
            {
                Wrapped = newChild;
                oldChild.Parent(null);
            }
            else throw new ArgumentException();
        }
    }
}