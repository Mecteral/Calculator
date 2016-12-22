namespace Calculator.Model
{
    public interface IExpression
    {
        IExpression Parent { get; set; }
        void Accept(IExpressionVisitor visitor);
    }
}