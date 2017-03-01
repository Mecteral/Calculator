using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Pipelines;

namespace Calculator.Logic
{
    public class WpfCalculationExecutor : IWpfCalculationExecutor
    {
        readonly IPipelineEvaluator mPipelineEvaluator;
        public WpfCalculationExecutor(IPipelineEvaluator pipelineEvaluator)
        {
            mPipelineEvaluator = pipelineEvaluator;
        }
        public string InitiateCalculation(string input)
        {
            return mPipelineEvaluator.Evaluate(input, null);
        }
    }

    public interface IWpfCalculationExecutor
    {
        string InitiateCalculation(string input);
    }
}
