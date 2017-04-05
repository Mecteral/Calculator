using System;
using Calculator.Logic.ArgumentParsing;
using Mecteral.UnitConversion;

namespace Calculator.Logic.Pipelines
{
    public class EvaluationPipeline : IEvaluationPipeline
    {
        readonly Func<IConversionFacade> mConversionFactory;
        readonly Func<ISimplificationPipeline> mSimplificationPipelineFactory;
        readonly IConsoleToMetricDecider mDecider;

        public EvaluationPipeline(Func<IConversionFacade> conversionFactory,
            Func<ISimplificationPipeline> simplificationPipelineFactory, IConsoleToMetricDecider decider)
        {
            mConversionFactory = conversionFactory;
            mSimplificationPipelineFactory = simplificationPipelineFactory;
            mDecider = decider;
        }

        public string Evaluate(string input, IApplicationArguments args)
        {
            if (input == null) return null;
            if (input.Contains("=?") || args.UseConversion)
            {
                mDecider.Decide();
                var conversion = mConversionFactory();
                return conversion.ConvertUnits(input, args.UnitForConversion, args.ToMetric);
            }
            var simplification = mSimplificationPipelineFactory();
            return simplification.UseSimplificationPipeline(input, args);
        }
    }
}