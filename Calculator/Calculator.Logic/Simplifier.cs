using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Logic.Model;
using Calculator.Model;

namespace Calculator.Logic
{
    public class Simplifier : ISimplifier
    {
        static IEnumerable<IExpression> AllPossibleSimplifications = new List<IExpression>();
        static IExpression SimplifiedCalculationExpression { get; set; }
        static IExpression ParentheseslessCalculationExpression { get; set; }
        static IExpression DirectCalculationExpression { get; set; }
        static IExpression ReorderedExpression { get; set; }
        static IExpression OriginalExpression { get; set; }
        static IExpression sSimplifiedExpression;
        public IExpression Simplify(IExpression input)
        {
            OriginalExpression = input;
            var equalityChecker = new ExpressionEqualityChecker();
            bool hasChanged;
            sSimplifiedExpression = ExpressionCloner.Clone(input);
            do
            {
                DirectCalculationExpression = DirectCalculationSimplifier.Simplify(sSimplifiedExpression);
                ParentheseslessCalculationExpression = ParenthesesSimplifier.Simplify(DirectCalculationExpression);
                if (!equalityChecker.IsEqual(sSimplifiedExpression, ParentheseslessCalculationExpression))
                {
                    sSimplifiedExpression = ExpressionCloner.Clone(ParentheseslessCalculationExpression);
                    hasChanged = true;
                }
                else
                    hasChanged = false;
            } while (hasChanged);
            return sSimplifiedExpression;
        }
    }
}