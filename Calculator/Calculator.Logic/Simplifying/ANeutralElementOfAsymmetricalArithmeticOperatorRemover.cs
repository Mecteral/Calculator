using Calculator.Logic.Model;
using Calculator.Model;
using November.MultiDispatch;

namespace Calculator.Logic.Simplifying
{
    public abstract class ANeutralElementOfAsymmetricalArithmeticOperatorRemover<T> where T : IArithmeticOperation
    {
        protected DoubleDispatcher<IExpression> Dispatcher { get; } = new DoubleDispatcher<IExpression>();
        readonly T mOperation;
        IExpression mResult;
        protected ANeutralElementOfAsymmetricalArithmeticOperatorRemover(T operation)
        {
            mOperation = operation;
            Dispatcher.OnRight<Constant>(IsNeutralElement).Do((lhs, _) => UseAsResult(lhs));
            Dispatcher.FallbackHandler = DoNothing;
        }
        protected abstract decimal NeutralElement { get; }
        static void DoNothing(IExpression lhs, IExpression rhs) {}
        protected bool IsNeutralElement(Constant c) => NeutralElement == c.Value;
        protected void UseAsResult(IExpression expression) => mResult = ExpressionCloner.Clone(expression);
        public IExpression Transform()
        {
            Dispatcher.Dispatch(mOperation.Left, mOperation.Right);
            return mResult ?? mOperation;
        }
    }
}