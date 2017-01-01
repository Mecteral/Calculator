namespace Calculator.Model
{
    public class Variable : AnExpression, IExpression
    {
        public string Variables { get; set; }
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}
