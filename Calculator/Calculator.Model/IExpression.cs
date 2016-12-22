namespace Calculator.Model
{
    public interface IExpression
    {
        IExpression Parent { get; }
        void Accept(IExpressionVisitor visitor);
    }
}