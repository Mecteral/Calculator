
namespace Calculator.Logic.CommandLineParser
{
    public class HelpText
    {
        static readonly string sUnitConversion = $"{"-u or --unit",-22}| specifies unit\n";
        static readonly string sConversionToDegree = $"{"-d or --degree",-22}| sets degrees\n";
        static readonly string sSteps = $"{"-s or --steps",-22}| sets if substeps are shown\n";
        static readonly string sWriter = $"{"-w or --writer",-22}| sets if switches are written to UserConfig\n";
        static readonly string sRevert = $"{"-r or --revert",-22}| sets if User config is set to default\n";
        static readonly string sImport= $"{"-i or --ímport",-22}| imports config from fiel path\n";
        static readonly string sCustom = $"{"-c or --custom",-22}| uses custom config from file path\n";
        static readonly string sSaveAll= $"{"-a or --saveAll",-22}| sets if all switches are saved to user config\n";
        public readonly string[] HelpStrings = {sUnitConversion, sConversionToDegree, sSteps, sWriter, sRevert, sImport, sCustom, sSaveAll, GetAttributeSnippet.Do()};
    }
}