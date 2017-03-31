using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class NeutralElementOfDivisionRemover : ANeutralElementOfAsymmetricalArithmeticOperatorRemover<Division>
    {
        public NeutralElementOfDivisionRemover(Division operation) : base(operation) {}
        protected override decimal NeutralElement => 1;
    }
}