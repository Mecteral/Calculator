namespace Calculator.Model
{
    public interface IExpressionWithName : IExpression
    {
        string Name { get; set; }
    }
}