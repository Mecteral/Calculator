using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class AZeroBasedNeutralElementOfArithmeticOperationRemover<T> : ANeutralElementOfArithmeticOperatorRemover<T>
        where T : IArithmeticOperation
    {
        protected AZeroBasedNeutralElementOfArithmeticOperationRemover(T operation) : base(operation) {}
        protected override decimal NeutralElement => 0;
    }
}