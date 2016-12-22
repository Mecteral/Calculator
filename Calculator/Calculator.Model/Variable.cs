namespace Calculator.Model
{
    public class Variable : IExpression
    {
        public IExpression Parent { get; set; }
        public string Variables { get; set; }
        public void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}
