using Calculator.Logic.Model;
using Calculator.Model;
using ModernRonin.PraeterArtem.Functional;

namespace Calculator.Logic.Simplifying
{
    public abstract class ATraversingReplacer : ISimplifier
    {
        IExpression mResult;
        public IExpression Simplify(IExpression input)
        {
            mResult = ExpressionCloner.Clone(input);
            Traverse(mResult);
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
            else original.Children.UseIn(Traverse);
        }
    }
}