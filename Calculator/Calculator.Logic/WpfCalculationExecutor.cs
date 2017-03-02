using System.Collections.Generic;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Model;
using Calculator.Logic.Pipelines;

namespace Calculator.Logic
{
    public class WpfCalculationExecutor : IWpfCalculationExecutor
    {
        public string CalculationResult { get; set; }
        public List<string> CalculationSteps { get; set; }
        readonly IPipelineEvaluator mPipelineEvaluator;

        public WpfCalculationExecutor(IPipelineEvaluator pipelineEvaluator, IApplicationArguments arguments)
        {
            mPipelineEvaluator = pipelineEvaluator;
            Arguments = arguments;
        }

        public void InitiateCalculation(string input)
        {
            CalculationResult = mPipelineEvaluator.Evaluate(input, Arguments);
            CalculationSteps = EvaluatingExpressionVisitor.Steps;
        }

        public IApplicationArguments Arguments { get; set; }
    }
}