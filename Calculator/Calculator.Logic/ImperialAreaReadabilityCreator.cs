using System.Collections.Generic;

namespace ImperialAndMetricConverter
{
    public class ImperialAreaReadabilityCreator : AnImperialReadabilityCreator
    {
        public string MakeReadable(IConversionExpressionWithValue expression)
        {
            IList<decimal> conversionFactors = new List<decimal>
            {
                ConversionFactors.AcreToSquareFoot,
                ConversionFactors.RoodToSquareFoot,
                ConversionFactors.PerchToSquareFoot,
                ConversionFactors.MultiplicationByOne
            };
            IList<string> abbreviations = new List<string>
            {
                UnitAbbreviations.Acre,
                UnitAbbreviations.Rood,
                UnitAbbreviations.Perch,
                UnitAbbreviations.Squarefoot
            };
            return CreateFrom(expression.Value, UnitAbbreviations.Squarefoot, conversionFactors, abbreviations);
        }
    }
}