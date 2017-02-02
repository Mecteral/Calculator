using System.Collections.Generic;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Parsing.CalculationTokenizer;

namespace Calculator.Logic.Facades
{
    public interface IEvaluationFacade
    {
        decimal Evaluate(ITokenizer token, ApplicationArguments args);
    }
}