using System.Collections.Generic;
using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic
{
    public interface IWpfCalculationExecutor
    {
        string CalculationResult { get; set; }
        List<string> CalculationSteps { get; set; }
        void InitiateCalculation(string input, IApplicationArguments arguments);
    }
}