using System;
using Calculator.Model;

namespace Calculator.Logic.Model
{
    public class EvaluatingExpressionVisitor : AnExpressionVisitorWithResult<EvaluatingExpressionVisitor, decimal>, IExpressionEvaluator
    {
        public decimal Evaluate(IExpression expression) => GetResultFor(expression);
        protected override decimal UseSinus(decimal value) => value;

        protected override decimal UseVariable(string variable)
        {
            throw new InvalidOperationException();
        }
        protected override decimal UseParenthesed(decimal wrapped) => wrapped;
        protected override decimal UseSubtraction(decimal left, decimal right) => left - right;
        protected override decimal UseMultiplication(decimal left, decimal right) => left * right;
        protected override decimal UseAddition(decimal left, decimal right) => left + right;
        protected override decimal UseDivision(decimal left, decimal right) => left / right;
        protected override decimal UseConstant(decimal value) => value;
        protected override decimal UseCosine(decimal value) => value;

        protected override decimal UseTangent(decimal value) => value;
    }
}