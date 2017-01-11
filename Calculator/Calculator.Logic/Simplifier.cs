using System.Collections.Generic;
using System.Linq;
using Calculator.Model;

namespace Calculator.Logic
{
    public class Simplifier : ISimplifier
    {
        readonly IList<ISimplifier> mSimplifiersInCorrectOrder = new List<ISimplifier>
        {
            new DirectCalculationSimplifier(),
            new ParenthesesSimplifier(),
            new AdditionAndSubtractionMover(),
            new VariableCalculator()
        };
        public IExpression Simplify(IExpression input)
        {
            var equalityChecker = new ExpressionEqualityChecker();

            var result = input;
            bool hasChanged;
            do
            {
                var transformed = ApplyAllSimplifications(result);

                hasChanged = !equalityChecker.IsEqual(result, transformed);
                if (hasChanged) { result = transformed; }
            }
            while (hasChanged);
            return result;
        }
        IExpression ApplyAllSimplifications(IExpression result)
        {
            return mSimplifiersInCorrectOrder.Aggregate(result, (current, simplifier) => simplifier.Simplify(current));
        }
    }
}