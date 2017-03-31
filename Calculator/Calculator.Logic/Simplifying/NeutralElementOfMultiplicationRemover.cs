using Calculator.Logic.Model;
using Calculator.Model;
using November.MultiDispatch;

namespace Calculator.Logic.Simplifying
{
    public class NeutralElementOfMultiplicationRemover
    {
        readonly DoubleDispatcher<IExpression> mDispatcher = new DoubleDispatcher<IExpression>();
        readonly Multiplication mMultiplication;
        IExpression mResult;
        public NeutralElementOfMultiplicationRemover(Multiplication multiplication)
        {
            mMultiplication = multiplication;
            mDispatcher.OnLeft<Constant>(IsNeutralElement).Do((_, rhs) => UseAsResult(rhs));
            mDispatcher.OnRight<Constant>(IsNeutralElement).Do((lhs, _) => UseAsResult(lhs));
            mDispatcher.FallbackHandler = DoNothing;
        }
        static void DoNothing(IExpression lhs, IExpression rhs) {}
        static bool IsNeutralElement(Constant c) => 1 == c.Value;
        void UseAsResult(IExpression expression) => mResult = ExpressionCloner.Clone(expression);
        public IExpression Transform()
        {
            mDispatcher.Dispatch(mMultiplication.Left, mMultiplication.Right);
            return mResult ?? mMultiplication;
        }
    }
}