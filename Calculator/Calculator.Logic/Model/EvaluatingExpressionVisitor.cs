using System;
using Calculator.Model;

namespace Calculator.Logic.Model
{
    public class EvaluatingExpressionVisitor : AnExpressionVisitorWithResult<EvaluatingExpressionVisitor, decimal>
    {
        public static decimal Evaluate(IExpression expression) => GetResultFor(expression);
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
    }
}