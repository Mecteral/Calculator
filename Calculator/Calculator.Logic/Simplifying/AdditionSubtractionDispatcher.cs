using System;
using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class AdditionSubtractionDispatcher
    {
        readonly IArithmeticOperation mOperation;
        readonly IArithmeticOperation mChainedOperation;
        public AdditionSubtractionDispatcher(IArithmeticOperation operation, IArithmeticOperation chainedOperation)
        {
            mOperation = operation;
            mChainedOperation = chainedOperation;
        }
        public Action<Addition, Addition> ForAddAdd { get; set; }
        public Action<Addition, Subtraction> ForAddSub { get; set; }
        public Action<Subtraction, Addition> ForSubAdd { get; set; }
        public Action<Subtraction, Subtraction> ForSubSub{ get; set; }
        public void Execute()
        {
            if (mOperation is Addition)
            {
                if (mChainedOperation is Addition) ForAddAdd((Addition) mOperation, (Addition) mChainedOperation);
                else if (mChainedOperation is Subtraction) ForAddSub((Addition) mOperation, (Subtraction) mChainedOperation);
                else throw new InvalidOperationException();
            }
            else if (mOperation is Subtraction)
            {
                if (mChainedOperation is Addition) ForSubAdd((Subtraction)mOperation, (Addition)mChainedOperation);
                else if (mChainedOperation is Subtraction) ForSubSub((Subtraction)mOperation, (Subtraction)mChainedOperation);
                else throw new InvalidOperationException();
            }
            else throw new InvalidOperationException();
        }
    }
}