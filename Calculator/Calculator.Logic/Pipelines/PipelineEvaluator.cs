using System;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Facades;

namespace Calculator.Logic.Pipelines
{
    public class PipelineEvaluator : IPipelineEvaluator
    {
        readonly Func<IConversionFacade> mConversionFactory;
        readonly Func<ISimplificationPipeline> mSimplificationPipelineFactory;
        public string Evaluate(string input, ApplicationArguments args)
        {
            if (input.Contains("=?"))
            {
                var conversion = mConversionFactory();
                return conversion.ConvertUnits(input, args);
            }
            else
            {
                var simplification = mSimplificationPipelineFactory();
                return simplification.UseSimplificationPipeline(input, args);
            }
        }

        public PipelineEvaluator(Func<IConversionFacade> conversionFactory, Func<ISimplificationPipeline> simplificationPipelineFactory)
        {
            mConversionFactory = conversionFactory;
            mSimplificationPipelineFactory = simplificationPipelineFactory;
        }
    }
}
