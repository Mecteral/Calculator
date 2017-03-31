using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class NeutralElementOfAdditionRemover : AZeroBasedNeutralElementOfArithmeticOperationRemover<Addition>
    {
        public NeutralElementOfAdditionRemover(Addition operation) : base(operation) {}
    }
}