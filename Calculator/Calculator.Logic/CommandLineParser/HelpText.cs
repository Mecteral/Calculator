using Calculator.Logic.Parsing.ConversionTokenizer;

namespace Calculator.Logic.CommandLineParser
{
    public class HelpText
    {
        static readonly string sUnitConversion = $"{"-u or --unit",-22}| specifies unit\n";
        static readonly string sConversionToDegree = $"{"-d or --degree",-22}| sets degrees\n";
        public readonly string[] HelpStrings = {sUnitConversion, sConversionToDegree, GetAttributeSnippet.Do()};
    }
}