using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class NeutralElementOfSubtractionRemover : AZeroBasedNeutralElementOfArithmeticOperationRemover<Subtraction>
    {
        public NeutralElementOfSubtractionRemover(Subtraction operation) : base(operation) {}
    }
}