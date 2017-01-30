using Calculator.Logic.Model;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Model;

namespace Calculator.Logic.Facades
{
    public class EvaluationFacade : IEvaluationFacade
    {
        readonly IModelBuilder mModelBuilder;
        public EvaluationFacade(IModelBuilder modelBuilder)
        {
            mModelBuilder = modelBuilder;
        }

        public decimal Evaluate(ITokenizer token)
        {
            return UseEvaluationExpressionVisitor(token);
        }

        IExpression CreateInMemoryModel(ITokenizer token) => mModelBuilder.BuildFrom(token.Tokens);

        decimal UseEvaluationExpressionVisitor(ITokenizer token)
            => EvaluatingExpressionVisitor.Evaluate(CreateInMemoryModel(token));
    }
}