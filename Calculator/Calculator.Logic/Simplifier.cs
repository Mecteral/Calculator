using System;
using Calculator.Model;

namespace Calculator.Logic
{
    public class Simplifier : ISimplifier
    {
        static IExpression SimplifiedCalculationExpression { get; set; }
        static IExpression DirectCalculationExpression { get; set; }
        static IExpression OriginalExpression { get; set; }
        public IExpression Simplify(IExpression input)
        {
            OriginalExpression = input;
            DirectCalculationExpression = DirectCalculationSimplifier.Simplify(input);
            return DirectCalculationExpression;
        }
    }
}