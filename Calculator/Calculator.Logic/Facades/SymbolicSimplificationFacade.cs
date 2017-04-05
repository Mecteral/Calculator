using Calculator.Logic.Model;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Logic.Simplifying;
using Calculator.Model;

namespace Calculator.Logic.Facades
{
    public class SymbolicSimplificationFacade : ISymbolicSimplificationFacade
    {
        readonly IExpressionFormatter mExpressionFormatter;
        readonly ISimplifier mSimplifier;
        readonly IModelBuilder mModelBuilder;
        public SymbolicSimplificationFacade(IExpressionFormatter expressionFormatter, ISimplifier simplify, IModelBuilder modelBuilder)
        {
            mExpressionFormatter = expressionFormatter;
            mSimplifier = simplify;
            mModelBuilder = modelBuilder;
        }

        public string Simplify(ITokenizer token)
        {
            return UseFormattingExpressionVisitor(UseSimplifier(token));
        }

        IExpression CreateInMemoryModel(ITokenizer token) => mModelBuilder.BuildFrom(token.Tokens);
        IExpression UseSimplifier(ITokenizer token) => mSimplifier.Simplify(CreateInMemoryModel(token));

        string UseFormattingExpressionVisitor(IExpression expression)
            => mExpressionFormatter.Format(expression);
    }
}