using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Parsing.ConversionTokenizer;

namespace CalculatorConsoleApplication
{
    public class HelpText
    {
        public readonly string[] HelpStrings = {sUnitConversion, sConversionToDegree, GetAttributeSnippet.Do()};
        //$"\n{"Units",4} {"Abbreviations",32}\n\n"
        static readonly string sUnitConversion = $"{"-u or --unit", -22}| specifies unit\n";
        static readonly string sConversionToDegree = $"{"-d or --degree",-22}| sets degrees\n";
    }
}
