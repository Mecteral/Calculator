using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class NeutralElementOfMultiplicationRemover :
        ANeutralElementOfSymmetricalArithmeticOperatorRemover<Multiplication>
    {
        public NeutralElementOfMultiplicationRemover(Multiplication multiplication) : base(multiplication) {}
        protected override decimal NeutralElement => 1;
    }
}