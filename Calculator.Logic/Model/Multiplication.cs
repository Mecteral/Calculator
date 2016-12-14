namespace Calculator.Logic.Model
{
    /// <summary>
    /// IExpression for Multiplication
    /// </summary>
    public class Multiplication : AnArithmeticOperation
    {
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}