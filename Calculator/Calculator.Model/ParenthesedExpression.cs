using System.Diagnostics;

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
                ((AnExpression) mWrapped).Parent = this;
                ((AnExpression) mWrapped).HasParent = true;
            }
        }
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
        public override string ToString() => $"({Wrapped})";
    }
}