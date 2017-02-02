namespace Calculator.Model
{
    /// <summary>
    /// IExpression for Division
    /// </summary>
    public class Division : AnArithmeticOperation
    {
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
        public override string ToString() => $"{Left}/{Right}";
    }

    public class Square : AnArithmeticOperation
    {
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
        public override string ToString() => $"{Left}^{Right}";
    }
}