using System;
using System.Globalization;
using System.Linq;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Facades;
using Calculator.Logic.Parsing.CalculationTokenizer;

namespace Calculator.Logic.Pipelines
{
    public class SimplificationPipeline : ISimplificationPipeline
    {
        readonly Func<IEvaluationFacade> mEvaluationFactory;
        readonly Func<ISymbolicSimplificationFacade> mSymbolicSimplificationFactory;
        readonly ITokenizer mTokenizer;

        public SimplificationPipeline(Func<IEvaluationFacade> evaluationFactory,
            Func<ISymbolicSimplificationFacade> symbolicSimplificationFactory, ITokenizer tokenizer)
        {
            mEvaluationFactory = evaluationFactory;
            mSymbolicSimplificationFactory = symbolicSimplificationFactory;
            mTokenizer = tokenizer;
        }

        public string UseSimplificationPipeline(string input, IApplicationArguments args)
        {
            var result = "";
            mTokenizer.Tokenize(input, args);
            if (IsSimplificationNecessary(mTokenizer))
            {
                var symbolicSimplifier = mSymbolicSimplificationFactory();
                result = symbolicSimplifier.Simplify(mTokenizer);
            }
            else
            {
                var evaluationSimplifier = mEvaluationFactory();
                result = evaluationSimplifier.Evaluate(mTokenizer, args).ToString(CultureInfo.InvariantCulture);
            }
            return result;
        }

        static bool IsSimplificationNecessary(ITokenizer tokenized) => tokenized.Tokens.OfType<VariableToken>().Any();
    }
}