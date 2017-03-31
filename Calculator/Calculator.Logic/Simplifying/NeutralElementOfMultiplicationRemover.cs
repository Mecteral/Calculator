using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class NeutralElementOfMultiplicationRemover :
        AOneBasedNeutralElementOfArithmeticOperationRemover<Multiplication>
    {
        public NeutralElementOfMultiplicationRemover(Multiplication multiplication) : base(multiplication) {}
    }
}