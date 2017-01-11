using System.Collections.Generic;
using System.Linq;
using Calculator.Logic.Model;
using Calculator.Model;

namespace Calculator.Logic
{
    public class Simplifier : ISimplifier
    {
        IEnumerable<ISimplifier> Simplifiers
        {
            get
            {
                yield return new DirectCalculationSimplifier();
                yield return new ParenthesesSimplifier();
                yield return new AdditionAndSubtractionMover();
                yield return new VariableCalculator();
            }
        }
        public IExpression Simplify(IExpression input)
        {
            var equalityChecker = new ExpressionEqualityChecker();

            bool hasChanged;
            var lastStep = ExpressionCloner.Clone(input);
            do
            {
                var transformed = Simplifiers.Aggregate(lastStep, (current, simplifier) => simplifier.Simplify(current));

                hasChanged = !equalityChecker.IsEqual(lastStep, transformed);
                if (hasChanged) { lastStep = transformed; }
            }
            while (hasChanged);
            return lastStep;
        }
        static string UseFormattingExpressionVisitor(IExpression expression)
            => new FormattingExpressionVisitor().Format(expression);
    }
}