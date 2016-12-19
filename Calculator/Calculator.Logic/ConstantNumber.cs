namespace Calculator.Logic
{
    public class ConstantNumber : IExpression
    {
        public double Value { get; set; }
        public double Evaluate()
        {
            return Value;
        }
    }
}