namespace Calculator.Logic
{
    public class Subtraction : AnArithmeticOperation
    {
        public override double Evaluate()
        {
            return Left.Evaluate() - Right.Evaluate();
        }
    }
}