namespace Calculator.Model
{
    public class SinusExpression : AnExpressionWithValue
    {
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void ReplaceChild(IExpression oldChild, IExpression newChild)
        {
            throw new System.NotImplementedException();
        }
    }
}