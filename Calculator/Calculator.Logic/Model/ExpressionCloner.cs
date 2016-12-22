using Calculator.Logic.Parsing;
using Calculator.Model;

namespace Calculator.Logic.Model
{
    public static class ExpressionCloner
    {
        public static IExpression Clone(IExpression expression)
        {
            var formatter = new FormattingExpressionVisitor();
            var token = new Tokenizer();
            token.Tokenize(formatter.Format(expression));
            return new ModelBuilder().BuildFrom(token.Tokens);
        }
    }
}