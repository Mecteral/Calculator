using System;
using Calculator.Model;

namespace Calculator.Logic.Model
{
    public class EvaluatingExpressionVisitor : AnExpressionVisitorWithResult<EvaluatingExpressionVisitor, double>
    {
        public static double Evaluate(IExpression expression) => GetResultFor(expression);
        protected override double UseVariable(string variable)
        {
            throw new InvalidOperationException();
        }
        protected override double UseParenthesed(double wrapped) => wrapped;
        protected override double UseSubtraction(double left, double right) => left - right;
        protected override double UseMultiplication(double left, double right) => left*right;
        protected override double UseAddition(double left, double right) => left + right;
        protected override double UseDivision(double left, double right) => left/right;
        protected override double UseConstant(double value) => value;
    }
}