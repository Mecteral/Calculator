using Calculator.Logic.Parsing.CalculationTokenizer;

namespace Calculator.Logic.Facades
{
    public interface IEvaluationFacade
    {
        decimal Evaluate(ITokenizer token);
    }
}