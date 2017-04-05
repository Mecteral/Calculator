using System.Collections.Generic;
using System.Linq;
using Calculator.Logic.Utilities;
using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class Simplifier : ISimplifier
    {
        readonly IEnumerable<ISimplifier> mSimplifiers;
        readonly IExpressionEqualityChecker mChecker;

        public Simplifier(IEnumerable<ISimplifier> simplifiers, IExpressionEqualityChecker checker)
        {
            mSimplifiers = simplifiers;
            mChecker = checker;
        }

        public IExpression Simplify(IExpression input)
        {
            var result = input;
            bool hasChanged;
            do
            {
                var transformed = ApplyAllSimplifications(result);
                hasChanged = !mChecker.IsEqual(result, transformed);
                if (hasChanged) { result = transformed; }
            }
            while (hasChanged);
            return result;
        }
        IExpression ApplyAllSimplifications(IExpression result)
        {
            return mSimplifiers.Aggregate(result, (current, simplifier) => simplifier.Simplify(current));
        }
    }
}