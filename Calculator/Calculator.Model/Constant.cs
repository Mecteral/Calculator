namespace Calculator.Model
{
    /// <summary>
    /// Is a Constant Number of type IExpression
    /// </summary>
    public class Constant : IExpression
    {
        public double Value { get; set; }
        public IExpression Parent { get; set; }
        public void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}