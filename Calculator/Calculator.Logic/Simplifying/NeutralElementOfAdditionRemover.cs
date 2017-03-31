using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class NeutralElementOfAdditionRemover : ANeutralElementOfSymmetricalArithmeticOperatorRemover<Addition>
    {
        public NeutralElementOfAdditionRemover(Addition operation) : base(operation) {}
        protected override decimal NeutralElement => 0;
    }
}