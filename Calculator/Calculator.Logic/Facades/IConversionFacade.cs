using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.Facades
{
    public interface IConversionFacade
    {
        string ConvertUnits(string input, ApplicationArguments args);
    }
}