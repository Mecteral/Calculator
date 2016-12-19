namespace Calculator.Logic
{
    public class Division: AnArithmeticOperation
    {
        public override double Evaluate()
        {
            return Left.Evaluate() / Right.Evaluate();
        }
    }
}