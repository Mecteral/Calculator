namespace Calculator.Model
{
    public interface IExpressionWithValue : IExpression
    {
        decimal Value { get; set; }
    }
}