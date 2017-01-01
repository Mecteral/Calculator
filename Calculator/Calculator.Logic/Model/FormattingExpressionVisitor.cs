using System.Globalization;
using Calculator.Model;

namespace Calculator.Logic.Model
{
    public class FormattingExpressionVisitor : AnExpressionVisitorWithResult<FormattingExpressionVisitor, string>, IExpressionFormatter
    {
        public string Format(IExpression expression) => GetResultFor(expression);
        protected override string UseVariable(string variable) => variable;
        protected override string UseParenthesed(string wrapped) => $"({wrapped})";

        protected override string UseSubtraction(string left, string right) => $"{left} - {right}";

        protected override string UseMultiplication(string left, string right) => $"{left}*{right}";

        protected override string UseAddition(string left, string right) => $"{left} + {right}";
        protected override string UseDivision(string left, string right) => $"{left}/{right}";

        protected override string UseConstant(double value) => value.ToString(CultureInfo.InvariantCulture);
    }
}