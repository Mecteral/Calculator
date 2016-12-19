using System.Globalization;

namespace Calculator.Logic.Model
{
    public class FormattingExpressionVisitor : AnExpressionVisitorWithResult<FormattingExpressionVisitor, string>
    {
        public static string Format(IExpression expression) => GetResultFor(expression);
        protected override string UseParenthesed(string wrapped) => $"({wrapped})";

        protected override string UseSubtraction(string left, string right) => $"{left} - {right}";

        protected override string UseMultiplication(string left, string right) => $"{left}*{right}";

        protected override string UseAddition(string left, string right) => $"{left} + {right}";
        protected override string UseDivision(string left, string right) => $"{left}/{right}";

        protected override string UseConstant(double value) => value.ToString(CultureInfo.InvariantCulture);
        protected override string UseVariable(double value, string variable) => $"{value.ToString(CultureInfo.InvariantCulture)} {variable}";
    }
}