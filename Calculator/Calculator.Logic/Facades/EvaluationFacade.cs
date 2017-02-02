using System;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Model;

namespace Calculator.Logic.Facades
{
    public class EvaluationFacade : IEvaluationFacade
    {
        readonly IModelBuilder mModelBuilder;
        readonly IExpressionEvaluator mExpressionEvaluator;
        public EvaluationFacade(IModelBuilder modelBuilder, IExpressionEvaluator expressionEvaluator)
        {
            mModelBuilder = modelBuilder;
            mExpressionEvaluator = expressionEvaluator;
        }

        public decimal Evaluate(ITokenizer token, ApplicationArguments args)
        {
            var expression = mModelBuilder.BuildFrom(token.Tokens);
            return mExpressionEvaluator.Evaluate(expression, args);
        }
    }
}