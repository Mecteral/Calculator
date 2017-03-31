using System.Collections.Generic;
using Calculator.Logic.Model;
using Calculator.Model;
using ModernRonin.PraeterArtem.Functional;

namespace Calculator.Logic.Simplifying
{
    public class NeutralElementEliminatingSimplifier : ISimplifier
    {
        public IExpression Simplify(IExpression input)
        {
            var multiplication = input as Multiplication;
            if (null == multiplication) return input;
            // TODO: do the same for division, and for addition/subtraction with zero
            return new NeutralElementOfMultiplicationRemover(multiplication).Transform();
        }
    }

    public abstract class ATraversingReplacer : ISimplifier
    {
        IExpression mResult;
        public IExpression Simplify(IExpression input)
        {
            mResult = ExpressionCloner.Clone(input);
            Traverse(input);
            return mResult;
        }
        protected abstract IExpression Replace(IExpression expression);
        void Traverse(IExpression original)
        {
            var replacement = Replace(original);
            if (replacement != original)
            {
                if (!original.HasParent) mResult = replacement;
                else original.Parent.ReplaceChild(original, replacement);
            }
            else
             GetChildren(original).UseIn(Traverse); 
        }
        IEnumerable<IExpression> GetChildren(IExpression expression)
        {
            yield break;
        }
    }
}