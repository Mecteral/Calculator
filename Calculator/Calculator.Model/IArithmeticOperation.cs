namespace Calculator.Model
{
    /// <summary>
    /// Interface for any Arithmetic Operation
    /// </summary>
    public interface IArithmeticOperation : IExpression
    {
        IExpression Left { get; set; }
        IExpression Right { get; set; }
    }
}