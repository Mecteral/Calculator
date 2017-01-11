using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Logic.Model;
using Calculator.Model;

namespace Calculator.Logic
{
    public class Simplifier : ISimplifier
    {
        public IExpression Simplify(IExpression input)
        {
            var equalityChecker = new ExpressionEqualityChecker();
            var mover = new AdditionAndSubtractionMover();
            var parenthesesSimplifier = new ParenthesesSimplifier();
            var variableCalculator = new VariableCalculator();
            var directCalculator = new DirectCalculationSimplifier();
            bool hasChanged;
            var lastStep = ExpressionCloner.Clone(input);
            do
            {
                var transformed = directCalculator.Simplify(lastStep);
                Console.WriteLine(UseFormattingExpressionVisitor(transformed));

                transformed = parenthesesSimplifier.Simplify(transformed);
                Console.WriteLine(UseFormattingExpressionVisitor(transformed));

                transformed = mover.Simplify(transformed);
                Console.WriteLine(UseFormattingExpressionVisitor(transformed));

                transformed = variableCalculator.Simplify(transformed);
                Console.WriteLine(UseFormattingExpressionVisitor(transformed));

                hasChanged = !equalityChecker.IsEqual(lastStep, transformed);
                if (hasChanged)
                {
                    lastStep = transformed;
                }
            } while (hasChanged);
            return lastStep;
        }
        static string UseFormattingExpressionVisitor(IExpression expression) => new FormattingExpressionVisitor().Format(expression);
    }
}