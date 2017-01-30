using Calculator.Logic.Parsing.CalculationTokenizer;

namespace Calculator.Logic.Facades
{
    public interface ISymbolicSimplificationFacade
    {
        string Simplify(ITokenizer token);
    }
}