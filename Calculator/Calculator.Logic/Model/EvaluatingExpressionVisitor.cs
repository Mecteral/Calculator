using System;
using System.Collections.Generic;
using Calculator.Logic.ArgumentParsing;
using Calculator.Model;

namespace Calculator.Logic.Model
{
    public class EvaluatingExpressionVisitor : AnExpressionVisitorWithResult<EvaluatingExpressionVisitor, decimal>, IExpressionEvaluator
    {
        public static List<string> Steps { get; private set; } = new List<string>();
        decimal Result { get; set; }
        public decimal Evaluate(IExpression expression, ApplicationArguments args)
        {
            Steps = new List<string>();
            Result = GetResultFor(expression);
            if (args != null && args.ShowSteps)
            {
                foreach (var step in Steps)
                {
                    Console.WriteLine(step);
                }
            }
            return Result;
        }
        protected override decimal UseSinus(decimal value) => value;
        protected override decimal UseSquareRoot(decimal value) => value;

        protected override decimal UseVariable(string variable)
        {
            throw new InvalidOperationException();
        }
        protected override decimal UseParenthesed(decimal wrapped) => wrapped;
        protected override decimal UseSubtraction(decimal left, decimal right) => left - right;
        protected override decimal UseMultiplication(decimal left, decimal right) => left * right;
        protected override decimal UseSquare(decimal left, decimal right) => (decimal) Math.Pow((double) left,(double) right);
        protected override decimal UseAddition(decimal left, decimal right) => left + right;
        protected override decimal UseDivision(decimal left, decimal right) => left / right;
        protected override decimal UseConstant(decimal value) => value;
        protected override decimal UseCosine(decimal value) => value;

        protected override decimal UseTangent(decimal value) => value;
    }
}