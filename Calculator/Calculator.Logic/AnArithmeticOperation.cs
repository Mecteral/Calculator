namespace Calculator.Logic
{
    public abstract class AnArithmeticOperation : IArithmeticOperation
    {
        public IExpression Left { get; set; }
        public IExpression Right { get; set; }
        public abstract double Evaluate();
    }
}