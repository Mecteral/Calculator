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
            var mover = new AdditionAndSubtractionMover();
            var variableCalculator = new VariableCalculator();
            bool hasChanged;
            sSimplifiedExpression = ExpressionCloner.Clone(input);
            do
            {
                DirectCalculationExpression = DirectCalculationSimplifier.Simplify(sSimplifiedExpression);
                ParentheseslessCalculationExpression = ParenthesesSimplifier.Simplify(DirectCalculationExpression);
                ReorderedExpression = mover.Move(ParentheseslessCalculationExpression);
                SimplifiedCalculationExpression = variableCalculator.Calculate(ReorderedExpression);
                if (!equalityChecker.IsEqual(sSimplifiedExpression, SimplifiedCalculationExpression))
                {
                    //Console.WriteLine(UseFormattingExpressionVisitor(sSimplifiedExpression));
                    sSimplifiedExpression = ExpressionCloner.Clone(SimplifiedCalculationExpression);
                    hasChanged = true;
                }
                else
                    hasChanged = false;
                sSimplifiedExpression = ExpressionCloner.Clone(SimplifiedCalculationExpression);
                Console.WriteLine(UseFormattingExpressionVisitor(DirectCalculationExpression));
                Console.WriteLine(UseFormattingExpressionVisitor(ParentheseslessCalculationExpression));
                Console.WriteLine(UseFormattingExpressionVisitor(ReorderedExpression));
                Console.WriteLine(UseFormattingExpressionVisitor(SimplifiedCalculationExpression));
                Console.WriteLine(UseFormattingExpressionVisitor(sSimplifiedExpression));
            } while (hasChanged);
            return sSimplifiedExpression;
        }
        static string UseFormattingExpressionVisitor(IExpression expression) => new FormattingExpressionVisitor().Format(expression);
    }
}