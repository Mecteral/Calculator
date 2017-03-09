namespace Calculator.Model
{
    public abstract class AnExpressionWithValue : AnExpression, IExpressionWithValue
    {
        public virtual decimal Value { get; set; }
    }
}