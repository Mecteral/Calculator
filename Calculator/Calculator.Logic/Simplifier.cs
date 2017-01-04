using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Model;

namespace Calculator.Logic
{
    public class Simplifier : ISimplifier
    {
        static IEnumerable<IExpression> AllPossibleSimplifications = new List<IExpression>();
        static IExpression SimplifiedCalculationExpression { get; set; }
        static IExpression DirectCalculationExpression { get; set; }
        static IExpression OriginalExpression { get; set; }
        static IExpression sSimplifiedExpression;
        public IExpression Simplify(IExpression input)
        {
            bool hasChanged;
            OriginalExpression = input;
            sSimplifiedExpression = input;
            do
            {
                DirectCalculationExpression = DirectCalculationSimplifier.Simplify(input);
                SimplifiedCalculationExpression = ParenthesesSimplifier.Simplify(DirectCalculationExpression);
                if (!sSimplifiedExpression.Equals(SimplifiedCalculationExpression))
                {
                    sSimplifiedExpression = SimplifiedCalculationExpression;
                    hasChanged = true;
                }
                else
                    hasChanged = false;
            } while (hasChanged);
            return sSimplifiedExpression;
        }
    }
}