namespace Calculator.Model
{
    public abstract class AnExpression : IExpression
    {
        public IExpression Parent { get; internal set; }
        public abstract void Accept(IExpressionVisitor visitor);
    }
}