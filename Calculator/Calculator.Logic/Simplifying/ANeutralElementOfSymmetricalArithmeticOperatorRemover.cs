using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public abstract class ANeutralElementOfSymmetricalArithmeticOperatorRemover<T> :
        ANeutralElementOfAsymmetricalArithmeticOperatorRemover<T> where T : IArithmeticOperation
    {
        protected ANeutralElementOfSymmetricalArithmeticOperatorRemover(T operation) : base(operation)
        {
            Dispatcher.OnLeft<Constant>(IsNeutralElement).Do((_, rhs) => UseAsResult(rhs));
        }
    }
}