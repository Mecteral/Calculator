using System.Collections.Generic;
using Calculator.Logic.Model.ConversionModel;
using Calculator.Logic.Parsing.ConversionTokenizer;

namespace Calculator.Logic.Conversion
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