namespace Calculator.Logic.Model
{
    public class EvaluatingExpressionVisitor : AnExpressionVisitorWithResult<EvaluatingExpressionVisitor, double>
    {
        public static double Evaluate(IExpression expression) => GetResultFor(expression);
        protected override double UseParenthesed(double wrapped) => wrapped;

        protected override double UseSubtraction(double left, double right) => left - right;

        protected override double UseMultiplication(double left, double right) => left*right;

        protected override double UseAddition(double left, double right) => left + right;

        protected override double UseDivision(double left, double right) => left/right;

        protected override double UseConstant(double value) => value;
        protected override double UseVariable(double value, string variable) => value;
    }
}