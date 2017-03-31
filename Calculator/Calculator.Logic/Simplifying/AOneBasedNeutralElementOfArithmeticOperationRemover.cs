using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class AOneBasedNeutralElementOfArithmeticOperationRemover<T> : ANeutralElementOfArithmeticOperatorRemover<T>
        where T : IArithmeticOperation
    {
        protected AOneBasedNeutralElementOfArithmeticOperationRemover(T operation) : base(operation) {}
        protected override decimal NeutralElement => 1;
    }
}