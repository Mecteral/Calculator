using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class NeutralElementOfDivisionRemover : AOneBasedNeutralElementOfArithmeticOperationRemover<Division>
    {
        public NeutralElementOfDivisionRemover(Division operation) : base(operation) {}
    }
}