using System.Collections.Generic;

namespace Calculator.Logic.Model
{
    /// <summary>
    /// Parenthesed Expression contains Wrapped IExpression
    /// </summary>
    public class ParenthesedExpression : IExpression
    {
        public IEnumerable<IExpression> Children { get; set; } = new List<IExpression>();
        public IExpression Parent { get; set; }
        public IExpression Wrapped { get; set; }
        public void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}