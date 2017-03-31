using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class NeutralElementOfSubtractionRemover : ANeutralElementOfSymmetricalArithmeticOperatorRemover<Subtraction>
    {
        public NeutralElementOfSubtractionRemover(Subtraction operation) : base(operation) {}
        protected override decimal NeutralElement => 0;
    }
}