using Calculator.Logic.Model;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Logic.Simplifying;
using Calculator.Model;

namespace Calculator.Logic.Facades
{
    public class SymbolicSimplificationFacade : ISymbolicSimplificationFacade
    {
        readonly IExpressionFormatter mExpressionFormatter;
        readonly ISimplify mSimplify;
        readonly IModelBuilder mModelBuilder;
        public SymbolicSimplificationFacade(IExpressionFormatter expressionFormatter, ISimplify simplify, IModelBuilder modelBuilder)
        {
            mExpressionFormatter = expressionFormatter;
            mSimplify = simplify;
            mModelBuilder = modelBuilder;
        }

        public string Simplify(ITokenizer token)
        {
            return UseFormattingExpressionVisitor(UseSimplifier(token));
        }

        IExpression CreateInMemoryModel(ITokenizer token) => mModelBuilder.BuildFrom(token.Tokens);
        IExpression UseSimplifier(ITokenizer token) => mSimplify.Simplify(CreateInMemoryModel(token));

        string UseFormattingExpressionVisitor(IExpression expression)
            => mExpressionFormatter.Format(expression);
    }
}