using System.Collections.Generic;
using System.Linq;
using Calculator.Logic.Model;
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

            bool hasChanged;
            var copyOfInput = ExpressionCloner.Clone(input);
            do
            {
                var transformed = mSimplifiersInCorrectOrder.Aggregate(copyOfInput, (current, simplifier) => simplifier.Simplify(current));

                hasChanged = !equalityChecker.IsEqual(copyOfInput, transformed);
                if (hasChanged) { copyOfInput = transformed; }
            }
            while (hasChanged);
            return copyOfInput;
        }
        static string UseFormattingExpressionVisitor(IExpression expression)
            => new FormattingExpressionVisitor().Format(expression);
    }
}