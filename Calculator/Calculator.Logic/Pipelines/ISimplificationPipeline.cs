using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.Pipelines
{
    public interface ISimplificationPipeline
    {
        string UseSimplificationPipeline(string input, ApplicationArguments args);
    }
}