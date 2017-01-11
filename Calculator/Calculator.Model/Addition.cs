namespace Calculator.Model
{
    /// <summary>
    /// IExpression for Addition
    /// </summary>
    public class Addition : AnArithmeticOperation
    {
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
        public override string ToString() => $"{Left}+{Right}";
    }
}