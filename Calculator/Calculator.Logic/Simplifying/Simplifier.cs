using System.Collections.Generic;
using System.Linq;
using Calculator.Logic.Utilities;
using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class Simplifier : ISimplify
    {
        readonly IList<ISimplifier> mSimplifiersInCorrectOrder = new List<ISimplifier>
        {
            new DirectCalculationSimplifier(),
            new ParenthesesSimplifier(),
            new AdditionAndSubtractionMover(),
            new VariableCalculator()
        };

        readonly IDirectCalculationSimplifier mDirect;
        readonly IParenthesesSimplifier mParentheses;
        readonly IAdditionAndSubtractionMover mMover;
        readonly IVariableCalculator mVariableCalculator;
        readonly IExpressionEqualityChecker mChecker;

        public Simplifier(){}
        Simplifier(IDirectCalculationSimplifier direct, IParenthesesSimplifier parentheses, IAdditionAndSubtractionMover mover, IVariableCalculator variableCalculator, IExpressionEqualityChecker checker)
        {
            mDirect = direct;
            mParentheses = parentheses;
            mMover = mover;
            mVariableCalculator = variableCalculator;
            mChecker = checker;
        }

        public IExpression Simplify(IExpression input)
        {
            var result = input;
            bool hasChanged;
            do
            {
                var transformed = mDirect.Simplify(input);
                transformed = mParentheses.Simplify(transformed);
                transformed = mMover.Simplify(transformed);
                transformed = mVariableCalculator.Simplify(transformed);

                hasChanged = !mChecker.IsEqual(result, transformed);
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

    public interface ISimplify
    {
        IExpression Simplify(IExpression input);
    }
}