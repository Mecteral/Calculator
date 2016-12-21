namespace Calculator.Logic.Model
{
    public interface IExpressionFormatter {
        string Format(IExpression expression);
    }
}