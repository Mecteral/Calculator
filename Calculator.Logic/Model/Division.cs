namespace Calculator.Logic.Model
{
    /// <summary>
    /// IExpression for Division
    /// </summary>
    public class Division : AnArithmeticOperation
    {
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}