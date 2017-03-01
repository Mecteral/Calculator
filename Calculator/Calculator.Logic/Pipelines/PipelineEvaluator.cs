using System;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Facades;
using Mecteral.UnitConversion;

namespace Calculator.Logic.Pipelines
{
    public class PipelineEvaluator : IPipelineEvaluator
    {
        readonly Func<IConversionFacade> mConversionFactory;
        readonly Func<ISimplificationPipeline> mSimplificationPipelineFactory;
        public string Evaluate(string input, ApplicationArguments args)
        {
            if (input == null) return null;
            if (input.Contains("=?"))
            {
                var toMetric = false;
                Console.WriteLine("Do you want to convert to the metric system? \n y, or yes for yes.");
                var metric = Console.ReadLine();
                if (metric == "y" || metric == "yes")
                {
                    toMetric = true;
                }
                var conversion = mConversionFactory();
                return conversion.ConvertUnits(input, args.UnitForConversion, toMetric);
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
