namespace Calculator.Logic
{
    public interface IArithmeticOperation : IExpression
    {
        IExpression Left { get; set; }
        IExpression Right { get; set; }
    }
}