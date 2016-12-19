namespace Calculator.Logic.Model
{
    public interface IExpression
    {
        void Accept(IExpressionVisitor visitor);
    }
}