using System.Collections.Generic;
using Calculator.Logic.Model;
using Calculator.Logic.Pipelines;

namespace Calculator.Logic
{
    public class WpfCalculationExecutor : IWpfCalculationExecutor
    {
        public string CalculationResult { get; set; }
        public List<string> CalculationSteps { get; set; }
        readonly IPipelineEvaluator mPipelineEvaluator;

        public WpfCalculationExecutor(IPipelineEvaluator pipelineEvaluator)
        {
            mPipelineEvaluator = pipelineEvaluator;
        }

        public void InitiateCalculation(string input)
        {
            CalculationResult = mPipelineEvaluator.Evaluate(input, null);
            CalculationSteps = EvaluatingExpressionVisitor.Steps;
        }
    }

    public interface IWpfCalculationExecutor
    {
        string CalculationResult { get; set; }
        List<string> CalculationSteps { get; set; }
        void InitiateCalculation(string input);
    }
}