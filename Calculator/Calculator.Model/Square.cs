namespace Calculator.Model
{
    public class Square : AnArithmeticOperation
    {
        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
        public override string ToString() => $"{Left}^{Right}";
    }
}