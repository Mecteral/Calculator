using Calculator.Logic.Model;
using Calculator.Model;
using November.MultiDispatch;

namespace Calculator.Logic.Simplifying
{
    public class NeutralElementOfMultiplicationRemover : ANeutralElementOfArithemethicOperatorRemover<Multiplication>
    {
        public NeutralElementOfMultiplicationRemover(Multiplication multiplication) : base(multiplication) {}
        protected override bool IsNeutralElement(Constant c) => 1 == c.Value;
    }

    public abstract class ANeutralElementOfArithemethicOperatorRemover<T> where T : IArithmeticOperation
    {
        readonly DoubleDispatcher<IExpression> mDispatcher = new DoubleDispatcher<IExpression>();
        readonly T mOperation;
        IExpression mResult;
        protected ANeutralElementOfArithemethicOperatorRemover(T operation)
        {
            mOperation = operation;
            mDispatcher.OnLeft<Constant>(IsNeutralElement).Do((_, rhs) => UseAsResult(rhs));
            mDispatcher.OnRight<Constant>(IsNeutralElement).Do((lhs, _) => UseAsResult(lhs));
            mDispatcher.FallbackHandler = DoNothing;
        }
        static void DoNothing(IExpression lhs, IExpression rhs) {}
        protected abstract bool IsNeutralElement(Constant c);
        void UseAsResult(IExpression expression) => mResult = ExpressionCloner.Clone(expression);
        public IExpression Transform()
        {
            mDispatcher.Dispatch(mOperation.Left, mOperation.Right);
            return mResult ?? mOperation;
        }
    }
}