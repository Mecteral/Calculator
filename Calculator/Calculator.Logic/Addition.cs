namespace Calculator.Logic
{
    public class Addition : AnArithmeticOperation
    {
        public override double Evaluate()
        {
            return Left.Evaluate() + Right.Evaluate();
        }
    }
}