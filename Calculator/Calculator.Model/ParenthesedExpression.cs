namespace Calculator.Model
{
    /// <summary>
    /// Parenthesed Expression contains Wrapped IExpression
    /// </summary>
    public class ParenthesedExpression : IExpression
    {
        IExpression mWrapped;
        public IExpression Parent { get; set; }
        public IExpression Wrapped
        {
            get { return mWrapped; }
            set
            {
                mWrapped = value;
                mWrapped.Parent = this;
            }
        }
        public void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}