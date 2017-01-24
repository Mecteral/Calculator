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
        public readonly string[] HelpStrings = {UnitConversion, ConversionToDegree, GetAttributeSnippet.Do()};
        const string UnitConversion = "-u or --unit   | specifies unit\n";
        const string ConversionToDegree = "-d or --degree | sets degrees\n";
    }
}
