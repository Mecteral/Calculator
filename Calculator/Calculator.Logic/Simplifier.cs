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
        static IExpression sSimplifiedExpression;
        public IExpression Simplify(IExpression input)
        {
            var originalExpression = input;
            var equalityChecker = new ExpressionEqualityChecker();
            var mover = new AdditionAndSubtractionMover();
            var variableCalculator = new VariableCalculator();
            bool hasChanged;
            sSimplifiedExpression = ExpressionCloner.Clone(input);
            do
            {
                var transformed = ExpressionCloner.Clone(DirectCalculationSimplifier.Simplify(sSimplifiedExpression));
                Console.WriteLine(UseFormattingExpressionVisitor(transformed));

                transformed = ExpressionCloner.Clone(ParenthesesSimplifier.Simplify(transformed));
                Console.WriteLine(UseFormattingExpressionVisitor(transformed));

                transformed = ExpressionCloner.Clone(mover.Move(transformed));
                Console.WriteLine(UseFormattingExpressionVisitor(transformed));

                transformed = ExpressionCloner.Clone(variableCalculator.Calculate(transformed));
                Console.WriteLine(UseFormattingExpressionVisitor(transformed));

                if (!equalityChecker.IsEqual(sSimplifiedExpression, transformed))
                {
                    //Console.WriteLine(UseFormattingExpressionVisitor(sSimplifiedExpression));
                    sSimplifiedExpression = ExpressionCloner.Clone(transformed);
                    hasChanged = true;
                }
                else
                    hasChanged = false;
                sSimplifiedExpression = ExpressionCloner.Clone(transformed);
            } while (hasChanged);
            return sSimplifiedExpression;
        }
        static string UseFormattingExpressionVisitor(IExpression expression) => new FormattingExpressionVisitor().Format(expression);
    }
}