using Calculator.Model;

namespace Calculator.Logic.Utilities
{
    public interface IExpressionEqualityChecker : IExpressionVisitor
    {
        bool IsEqual(IExpression firstExpression, IExpression secondExpression);
    }
}