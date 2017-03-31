using Calculator.Logic.Model;
using Calculator.Model;
using November.MultiDispatch;

namespace Calculator.Logic.Simplifying
{
    public class AOneBasedNeutralElementOfArithmeticOperationRemover<T> : ANeutralElementOfArithmeticOperatorRemover<T>
        where T : IArithmeticOperation
    {
        protected AOneBasedNeutralElementOfArithmeticOperationRemover(T operation) : base(operation) {}
        protected override decimal NeutralElement => 1;
    }

    public class AZeroBasedNeutralElementOfArithmeticOperationRemover<T> : ANeutralElementOfArithmeticOperatorRemover<T>
        where T : IArithmeticOperation
    {
        protected AZeroBasedNeutralElementOfArithmeticOperationRemover(T operation) : base(operation) {}
        protected override decimal NeutralElement => 0;
    }

    public class NeutralElementOfAdditionRemover : AZeroBasedNeutralElementOfArithmeticOperationRemover<Addition>
    {
        public NeutralElementOfAdditionRemover(Addition operation) : base(operation) {}
    }

    public class NeutralElementOfSubtractionRemover : AZeroBasedNeutralElementOfArithmeticOperationRemover<Subtraction>
    {
        public NeutralElementOfSubtractionRemover(Subtraction operation) : base(operation) {}
    }

    public class NeutralElementOfMultiplicationRemover :
        AOneBasedNeutralElementOfArithmeticOperationRemover<Multiplication>
    {
        public NeutralElementOfMultiplicationRemover(Multiplication multiplication) : base(multiplication) {}
    }

    public class NeutralElementOfDivisionRemover : AOneBasedNeutralElementOfArithmeticOperationRemover<Division>
    {
        public NeutralElementOfDivisionRemover(Division operation) : base(operation) {}
    }

    public abstract class ANeutralElementOfArithmeticOperatorRemover<T> where T : IArithmeticOperation
    {
        readonly DoubleDispatcher<IExpression> mDispatcher = new DoubleDispatcher<IExpression>();
        readonly T mOperation;
        IExpression mResult;
        protected ANeutralElementOfArithmeticOperatorRemover(T operation)
        {
            mOperation = operation;
            mDispatcher.OnLeft<Constant>(IsNeutralElement).Do((_, rhs) => UseAsResult(rhs));
            mDispatcher.OnRight<Constant>(IsNeutralElement).Do((lhs, _) => UseAsResult(lhs));
            mDispatcher.FallbackHandler = DoNothing;
        }
        protected abstract decimal NeutralElement { get; }
        static void DoNothing(IExpression lhs, IExpression rhs) {}
        bool IsNeutralElement(Constant c) => NeutralElement == c.Value;
        void UseAsResult(IExpression expression) => mResult = ExpressionCloner.Clone(expression);
        public IExpression Transform()
        {
            mDispatcher.Dispatch(mOperation.Left, mOperation.Right);
            return mResult ?? mOperation;
        }
    }
}