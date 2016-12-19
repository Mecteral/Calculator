namespace Calculator.Logic
{
    public class Multiplication: AnArithmeticOperation
    {
        public override double Evaluate()
        {
            return Left.Evaluate() * Right.Evaluate();
        }
    }
}