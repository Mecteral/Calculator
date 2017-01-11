using System;
using System.Collections.Generic;
using Calculator.Model;

namespace Calculator.Logic
{
    public class Dispatcher
    {
        readonly Dictionary<Type, Dictionary<Type, Action<object, object>>> mHandlers =
            new Dictionary<Type, Dictionary<Type, Action<object, object>>>();
        public Action<IExpression, IExpression> FallbackHandler { get; set; }
        public LeftContinuation<TLeft> OnLeft<TLeft>()
        {
            return new LeftContinuation<TLeft>(this);
        }
        public void On<TLeft, TRight>(Action<TLeft, TRight> handler)
        {
            AddHandler(typeof(TLeft), typeof(TRight), ToUntypedAction(handler));
        }
        public void Dispatch(IExpression left, IExpression right)
        {
            var leftType = left.GetType();
            var rightType = right.GetType();
            if (!mHandlers.ContainsKey(leftType)) InvokeFallbackHandler(left, right);
            else
            {
                var leftHandlers = mHandlers[leftType];
                if (!leftHandlers.ContainsKey(rightType)) InvokeFallbackHandler(left, right);
                else leftHandlers[rightType](left, right);
            }
        }
        void InvokeFallbackHandler(IExpression left, IExpression right)
        {
            if (null == FallbackHandler)
                throw new InvalidOperationException(
                    $"You must either take care to define handlers for all permutations that might come in; or define '{nameof(FallbackHandler)}'.");
            FallbackHandler(left, right);
        }
        void AddHandler(Type leftType, Type rightType, Action<object, object> action)
        {
            if (!mHandlers.ContainsKey(leftType)) mHandlers[leftType] = new Dictionary<Type, Action<object, object>>();
            var leftHandlers = mHandlers[leftType];
            leftHandlers[rightType] = action;
        }
        static Action<object, object> ToUntypedAction<TLeft, TRight>(Action<TLeft, TRight> action)
            => (l, r) => action((TLeft) l, (TRight) r);

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
            public void Do(Action<TLeft, TRight> handler)
            {
                mDispatcher.AddHandler(typeof(TLeft), typeof(TRight), ToUntypedAction(handler));
            }
        }
    }
}