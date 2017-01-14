using System.Collections.Generic;
using System.Globalization;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public abstract class AConversionTokens
    {
        readonly Dictionary<string, decimal> mMap = new Dictionary<string, decimal>
        {
            //Metric Length
            {UnitAbbreviations.Millimeters, ConversionFactors.MetricDivisionOneThousand},
            {UnitAbbreviations.Centimeters, ConversionFactors.MetricDivisionOneHundred},
            {UnitAbbreviations.Meters, ConversionFactors.MultiplicationByOne},
            {UnitAbbreviations.Kilometers, ConversionFactors.MetricMultiplicationOneThousand},
            //Metric Mass
            {UnitAbbreviations.Milligram, ConversionFactors.MetricDivisionOneThousand},
            {UnitAbbreviations.Gram, ConversionFactors.MultiplicationByOne},
            {UnitAbbreviations.Kilogram, ConversionFactors.MetricMultiplicationOneThousand},
            {UnitAbbreviations.Ton, ConversionFactors.MetricMultiplicationOneMillion},
            //Metric Volume
            {UnitAbbreviations.Milliliters, ConversionFactors.MetricDivisionOneThousand},
            {UnitAbbreviations.Centiliters, ConversionFactors.MetricDivisionOneHundred},
            {UnitAbbreviations.Liters, ConversionFactors.MultiplicationByOne},
            {UnitAbbreviations.Hectoliters, ConversionFactors.MetricMultiplicationOneHundred},
            //Metric Area
            {UnitAbbreviations.Squaremillimeters, ConversionFactors.MetricDivisionOneMillion},
            {UnitAbbreviations.Squarecentimeters, ConversionFactors.MetricDivisionOneThousand},
            {UnitAbbreviations.Sqauremeters, ConversionFactors.MultiplicationByOne},
            {UnitAbbreviations.Squarekilometers, ConversionFactors.MetricMultiplicationOneMillion},
            {UnitAbbreviations.Hectas, ConversionFactors.MetricMultiplicationMeterToha},
            //Imperial Length
            {UnitAbbreviations.Though, ConversionFactors.ThouToFeet},
            {UnitAbbreviations.Inch, ConversionFactors.InchToFeet},
            {UnitAbbreviations.Foot, ConversionFactors.MultiplicationByOne},
            {UnitAbbreviations.Yard, ConversionFactors.YardToFeet},
            {UnitAbbreviations.Chain, ConversionFactors.ChainToFeet},
            {UnitAbbreviations.Furlong, ConversionFactors.FurlongToFeet},
            {UnitAbbreviations.Mile, ConversionFactors.MileToFeet},
            {UnitAbbreviations.League, ConversionFactors.LeagueToFeet},
            {UnitAbbreviations.Fathom, ConversionFactors.FathomToFeet},
            //Imperial Area
            {UnitAbbreviations.Squarefoot, ConversionFactors.MultiplicationByOne},
            {UnitAbbreviations.Perch, ConversionFactors.PerchToSquareFoot},
            {UnitAbbreviations.Rood, ConversionFactors.RoodToSquareFoot},
            {UnitAbbreviations.Acre, ConversionFactors.AcreToSquareFoot},
            //Imperial Volume
            {UnitAbbreviations.FluidOunce, ConversionFactors.MultiplicationByOne},
            {UnitAbbreviations.Gill, ConversionFactors.GillToFluidOunce},
            {UnitAbbreviations.Pint, ConversionFactors.PintToFluidOunce},
            {UnitAbbreviations.Quart, ConversionFactors.QuartToFluidOunce},
            {UnitAbbreviations.Gallon, ConversionFactors.GallonToFluidOunce},
            //Imperial Mass
            {UnitAbbreviations.Grain, ConversionFactors.GrainToPound},
            {UnitAbbreviations.Drachm, ConversionFactors.DrachmToPound},
            {UnitAbbreviations.Ounce, ConversionFactors.OunceToPound},
            {UnitAbbreviations.Pound, ConversionFactors.MultiplicationByOne},
            {UnitAbbreviations.Stone, ConversionFactors.StoneToPound},
            {UnitAbbreviations.HundredWeight, ConversionFactors.HundredWeightToPound},
            {UnitAbbreviations.ImperialTon, ConversionFactors.ImperialTonToPound}
        };

        decimal mNumber;
        string mNumberAsText;
        string mUnit;

        protected AConversionTokens(string asText)
        {
            asText = asText.Replace(',', '.');


            foreach (var c in asText)
            {
                if (char.IsNumber(c) || c == '.')
                {
                    ConvertIfPossible();
                    mNumberAsText += c;
                }
                else if (char.IsLetter(c))
                {
                    ParseIfPossible();
                    mUnit += c;
                }
            }
            ConvertIfPossible();
        }

        public decimal Value { get; private set; }

        void ConvertIfPossible()
        {
            if (mUnit != null)
            {
                ConvertToUnitAndAddToValue();
            }
        }

        void ParseIfPossible()
        {
            if (mNumberAsText != null)
            {
                mNumber = decimal.Parse(mNumberAsText, NumberStyles.Any, CultureInfo.InvariantCulture);
                mNumberAsText = null;
            }
        }

        void ConvertToUnitAndAddToValue()
        {
            Value += mNumber*mMap[mUnit];
            mUnit = null;
            mNumber = 0;
        }
    }
}