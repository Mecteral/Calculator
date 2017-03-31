using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class NeutralElementOfSubtractionRemover : ANeutralElementOfAsymmetricalArithmeticOperatorRemover<Subtraction>
    {
        public NeutralElementOfSubtractionRemover(Subtraction operation) : base(operation) {}
        protected override decimal NeutralElement => 0M;
    }
}