using System.Collections.Generic;

namespace ImperialAndMetricConverter
{
    public class ImperialMassReadabilityCreator : AnImperialReadabilityCreator
    {
        public string MakeReadable(IConversionExpressionWithValue expression)
        {
            IList<decimal> conversionFactors = new List<decimal>
            {
                ConversionFactors.ImperialTonToPound,
                ConversionFactors.HundredWeightToPound,
                ConversionFactors.StoneToPound,
                ConversionFactors.MultiplicationByOne,
                ConversionFactors.OunceToPound,
                ConversionFactors.DrachmToPound,
                ConversionFactors.GrainToPound
            };
            IList<string> abbreviations = new List<string>
            {
                UnitAbbreviations.ImperialTon,
                UnitAbbreviations.HundredWeight,
                UnitAbbreviations.Stone,
                UnitAbbreviations.Pound,
                UnitAbbreviations.Ounce,
                UnitAbbreviations.Drachm,
                UnitAbbreviations.Grain
            };
            return CreateFrom(expression.Value, UnitAbbreviations.Pound, conversionFactors, abbreviations);
        }
    }
}