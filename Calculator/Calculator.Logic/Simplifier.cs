using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Model;

namespace Calculator.Logic
{
    public class Simplifier : ISimplifier
    {
        IEnumerable<IExpression> AllPossibleSimplifications = new List<IExpression>();
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