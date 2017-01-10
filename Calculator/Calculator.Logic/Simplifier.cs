using System;
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
            var variableCalculator = new VariableCalculator();
            bool hasChanged;
            var lastStep = ExpressionCloner.Clone(input);
            do
            {
                var transformed = DirectCalculationSimplifier.Simplify(lastStep);
                Console.WriteLine(UseFormattingExpressionVisitor(transformed));

                transformed = ParenthesesSimplifier.Simplify(transformed);
                Console.WriteLine(UseFormattingExpressionVisitor(transformed));

                transformed = mover.Move(transformed);
                Console.WriteLine(UseFormattingExpressionVisitor(transformed));

                transformed = variableCalculator.Calculate(transformed);
                Console.WriteLine(UseFormattingExpressionVisitor(transformed));

                if (!equalityChecker.IsEqual(lastStep, transformed))
                {
                    lastStep = transformed;
                    hasChanged = true;
                }
                else
                    hasChanged = false;
            } while (hasChanged);
            return lastStep;
        }
        static string UseFormattingExpressionVisitor(IExpression expression) => new FormattingExpressionVisitor().Format(expression);
    }
}