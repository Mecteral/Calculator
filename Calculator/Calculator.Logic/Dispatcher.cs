using System;
using System.Collections.Generic;
using Calculator.Model;

namespace Calculator.Logic
{
    public class Dispatcher
    {
        readonly Dictionary<Type, Dictionary<Type, Action<object, object>>> mHandlers =
            new Dictionary<Type, Dictionary<Type, Action<object, object>>>();
        readonly IExpression mLeft;
        readonly IExpression mRight;
        public Dispatcher(IExpression left, IExpression right)
        {
            mLeft = left;
            mRight = right;
        }
        public Action<IExpression, IExpression> FallbackHandler { get; set; }
        public LeftContinuation<TLeft> OnLeft<TLeft>()
        {
            return new LeftContinuation<TLeft>(this);
        }
        public void Dispatch()
        {
            var leftType = mLeft.GetType();
            var rightType = mRight.GetType();
            if (!mHandlers.ContainsKey(leftType)) FallbackHandler(mLeft, mRight);
            else
            {
                var leftHandlers = mHandlers[leftType];
                if (!leftHandlers.ContainsKey(rightType)) FallbackHandler(mLeft, mRight);
                else leftHandlers[rightType](mLeft, mRight);
            }
        }
        void AddHandler(Type leftType, Type rightType, Action<object, object> action)
        {
            if (!mHandlers.ContainsKey(leftType)) mHandlers[leftType] = new Dictionary<Type, Action<object, object>>();
            var leftHandlers = mHandlers[leftType];
            leftHandlers[rightType] = action;
        }
        public class LeftContinuation<TLeft>
        {
            readonly Dispatcher mDispatcher;
            public LeftContinuation(Dispatcher dispatcher)
            {
                mDispatcher = dispatcher;
            }
            public RightContinuation<TLeft, TRight> OnRight<TRight>()
            {
                return new RightContinuation<TLeft, TRight>(mDispatcher);
            }
        }

        public class RightContinuation<TLeft, TRight>
        {
            readonly Dispatcher mDispatcher;
            public RightContinuation(Dispatcher dispatcher)
            {
                mDispatcher = dispatcher;
            }
            public void Do(Action<TLeft, TRight> action)
            {
                mDispatcher.AddHandler(typeof(TLeft), typeof(TRight), (l, r) => action((TLeft)l, (TRight)r));
            }
        }
    }
}