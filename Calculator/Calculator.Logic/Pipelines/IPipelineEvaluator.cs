using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.Pipelines
{
    public interface IPipelineEvaluator
    {
        string Evaluate(string input, ApplicationArguments args);
    }
}