using System.Collections.Generic;
using Calculator.Logic.Model.ConversionModel;
using Calculator.Logic.Parsing.ConversionTokenizer;

namespace Calculator.Logic
{
    public class ImperialVolumeReadabilityCreator : AnImperialReadabilityCreator
    {
        public string MakeReadable(IConversionExpressionWithValue expression)
        {
            IList<decimal> conversionFactors = new List<decimal>
            {
                ConversionFactors.GallonToFluidOunce,
                ConversionFactors.QuartToFluidOunce,
                ConversionFactors.PintToFluidOunce,
                ConversionFactors.GillToFluidOunce,
                ConversionFactors.MultiplicationByOne
            };
            IList<string> abbreviations = new List<string>
            {
                UnitAbbreviations.Gallon,
                UnitAbbreviations.Quart,
                UnitAbbreviations.Pint,
                UnitAbbreviations.Gill,
                UnitAbbreviations.FluidOunce
            };
            return CreateFrom(expression.Value, UnitAbbreviations.FluidOunce, conversionFactors, abbreviations);
        }
    }
}