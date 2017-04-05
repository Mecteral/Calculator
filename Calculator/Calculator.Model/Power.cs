namespace Calculator.Model
{
    public class Power : AnArithmeticOperation
    {
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
        public override string ToString() => $"{Left}^{Right}";
    }
}