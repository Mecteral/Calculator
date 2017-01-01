namespace Calculator.Model
{
    /// <summary>
    /// IExpression for Subtraction
    /// </summary>
    public class Subtraction : AnArithmeticOperation
    {
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}