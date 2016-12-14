namespace Calculator.Logic.Model
{
    /// <summary>
    /// Parenthesed Expression contains Wrapped IExpression
    /// </summary>
    public class ParenthesedExpression : IExpression
    {
        public IExpression Wrapped { get; set; }
        public void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}