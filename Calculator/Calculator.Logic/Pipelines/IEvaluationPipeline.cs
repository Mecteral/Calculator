using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.Pipelines
{
    public interface IEvaluationPipeline
    {
        string Evaluate(string input, IApplicationArguments args);
    }
}