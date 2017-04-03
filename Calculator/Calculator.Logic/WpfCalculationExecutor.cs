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
        readonly IEvaluationPipeline mPipelineEvaluator;

        public WpfCalculationExecutor(IEvaluationPipeline pipelineEvaluator)
        {
            mPipelineEvaluator = pipelineEvaluator;
        }

        public void InitiateCalculation(string input, IApplicationArguments arguments)
        {
            CalculationResult = mPipelineEvaluator.Evaluate(input, arguments);
            CalculationSteps = EvaluatingExpressionVisitor.Steps;
        }

    }
}