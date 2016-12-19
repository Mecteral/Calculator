namespace Calculator.Logic
{
    public class ParentheseExpression : IExpression
    {
        public IExpression Inner { get; set; }

        public double Evaluate()
        {
            return Inner.Evaluate();
        }
    }
}