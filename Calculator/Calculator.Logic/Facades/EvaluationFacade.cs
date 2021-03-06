﻿using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing.CalculationTokenizer;

namespace Calculator.Logic.Facades
{
    public class EvaluationFacade : IEvaluationFacade
    {
        readonly IExpressionEvaluator mExpressionEvaluator;
        readonly IModelBuilder mModelBuilder;

        public EvaluationFacade(IModelBuilder modelBuilder, IExpressionEvaluator expressionEvaluator)
        {
            mModelBuilder = modelBuilder;
            mExpressionEvaluator = expressionEvaluator;
        }

        public decimal Evaluate(ITokenizer token, IApplicationArguments args)
        {
            var expression = mModelBuilder.BuildFrom(token.Tokens);
            return mExpressionEvaluator.Evaluate(expression, args);
        }
    }
}