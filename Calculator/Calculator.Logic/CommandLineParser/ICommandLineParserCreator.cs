using Calculator.Logic.ArgumentParsing;
using Fclp;

namespace Calculator.Logic.CommandLineParser
{
    public interface ICommandLineParserCreator
    {
        FluentCommandLineParser ArgumentsSetup(ApplicationArguments args);
    }
}