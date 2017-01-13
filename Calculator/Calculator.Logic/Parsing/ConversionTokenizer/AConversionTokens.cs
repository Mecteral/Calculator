using System.Collections.Generic;
using System.Globalization;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public abstract class AConversionTokens
    {
        
        public decimal Value { get; private set; } = 0;
        string mUnit;
        decimal mNumber;
        string mNumberAsText;

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
        void ConvertIfPossible()
        {
            if (mUnit != null)
            {
                ConvertToMetersAndAddToValue();
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
        void ConvertToMetersAndAddToValue()
        {

            switch (mUnit)
            {
                case "mm":
                    Value += mNumber * (decimal)1.0E-3;
                    break;
                case "cm":
                    Value += mNumber * (decimal)1.0E-2;
                    break;
                case "m":
                    Value += mNumber;
                    break;
                case "km":
                    Value += mNumber * 1000;
                    break;
                case "ml":
                    Value += mNumber * (decimal)1.0E-3;
                    break;
                case "cl":
                    Value += mNumber * (decimal)1.0E-2;
                    break;
                case "l":
                    Value += mNumber;
                    break;
                case "hl":
                    Value += mNumber * 100;
                    break;
                case "mg":
                    Value += mNumber*(decimal) 1.0E-2;
                    break;
                case "g":
                    Value += mNumber;
                    break;
                case "kg":
                    Value += mNumber * 1000;
                    break;
                case "t":
                    Value += mNumber * (decimal)1.0E6;
                    break;
                case "qmm":
                    Value += mNumber * (decimal)1.0E-6;
                    break;
                case "qcm":
                    Value += mNumber * (decimal)1.0E-3;
                    break;
                case "qm":
                    Value += mNumber;
                    break;
                case "qkm":
                    Value += mNumber * (decimal)1.0E6;
                    break;
                case "ha":
                    Value += mNumber * 10000;
                    break;
                case "th":
                    Value += mNumber / 12000;
                    break;
                case "in":
                    Value += mNumber / 12;
                    break;
                case "ft":
                    Value += mNumber;
                    break;
                case "yd":
                    Value += mNumber * 3;
                    break;
                case "ch":
                    Value += mNumber *66;
                    break;
                case "fur":
                    Value += mNumber *660;
                    break;
                case "mI":
                    Value += mNumber * 5280;
                    break;
                case "lea":
                    Value += mNumber * 15840;
                    break;
                case "ftm":
                    Value += mNumber * 608001;
                    break;
                case "sft":
                    Value += mNumber;
                    break;
                case "perch":
                    Value += mNumber * (decimal) 272.25;
                    break;
                case "rood":
                    Value += mNumber * 10890;
                    break;
                case "acre":
                    Value += mNumber * 43560;
                    break;
                case "floz":
                    Value += mNumber;
                    break;
                case "gi":
                    Value += mNumber * 5;
                    break;
                case "pt":
                    Value += mNumber * 20;
                    break;
                case "qt":
                    Value += mNumber * 40;
                    break;
                case "gal":
                    Value += mNumber * 160;
                    break;
                case "gr":
                    Value += mNumber * 1/7000;
                    break;
                case "dr":
                    Value += mNumber * 1/256;
                    break;
                case "oz":
                    Value += mNumber * 1/16;
                    break;
                case "lb":
                    Value += mNumber;
                    break;
                case "st":
                    Value += mNumber * 14;
                    break;
                case "cwt":
                    Value += mNumber * 112;
                    break;
                case "it":
                    Value += mNumber * 2240;
                    break;

            }
            mUnit = null;
            mNumber = 0;
        }
    }

    public class UnitAbbreviations

    {
        //Metric Length
        public const string Millimeters = "mm";
        public const string Centimeters = "cm";
        public const string Meters = "m";
        public const string Kilometers = "km";
        public const string Milligram = "mg";
        public const string Gram = "g";
        public const string Kilogram = "kg";
        public const string Ton = "t";
        public const string Milliliters = "ml";
        public const string Centiliters = "cl";
        public const string Liters = "l";
        public const string Hectoliters = "hl";
        public const string Squaremillimeters = "qmm";
        public const string Squarecentimeters = "qcm";
        public const string Sqauremeters = "qm";
        public const string Squarekilometers = "qkm";
        public const string Hectas = "ha";
        //Imperial Length
        public const string Though = "th";
        public const string Inch = "in";
        public const string Foot = "ft";
        public const string Yard = "yd";
        public const string Chain = "ch";
        public const string Furlong = "fur";
        public const string Mile = "mI";
        public const string League = "lea";
        public const string Fathom = "ftm";
        //Imperial Area
        public const string Squarefoot = "sft";
        public const string Perch = "perch";
        public const string Rood = "rood";
        public const string Acre= "acre";
        //Imperial Volume
        public const string FluidOunce = "floz";
        public const string Gill = "gi";
        public const string Pint = "pt";
        public const string Quart = "qt";
        public const string Gallon = "gal";
        //Imperial Mass
        public const string Grain = "gr";
        public const string Drachm = "dr";
        public const string Ounce = "oz";
        public const string Pound = "lb";
        public const string Stone = "st";
        public const string HundredWeight = "cwt";
        public const string ImperialTon = "it";
    }

    public class ConversionFactors

    {
        //Metric
        public const decimal MetricDivisionOneThousand = (decimal)1.0E-3;
        public const decimal MetricDivisionOneHundred = (decimal)1.0E-2;
        public const decimal MetricMultiplicationOneThousand = (decimal)1.0E3;
        public const decimal MetricMultiplicationOneMillion = (decimal)1.0E6;
        public const decimal MetricMultiplicationMeterToha = (decimal)1.0E5;
        //Imperial Length
        public const decimal ThouToFeet = (decimal)1/12000;
        public const decimal InchToFeet = (decimal)1/12;
        public const decimal FeetToFeet = 1;
        public const decimal YardToFeet = 3;
        public const decimal ChainToFeet = 66;
        public const decimal FurlongToFeet = 660;
        public const decimal MileToFeet = 5280;
        public const decimal LeagueToFeet = 15840;
        public const decimal FathomToFeet = 608001;
        //Imperial Area
        public const decimal SquareFoot = 1;
        public const decimal PerchToSquareFoot = (decimal) 272.25;
        public const decimal RoodToSquareFoot = 10890;
        public const decimal AcreToSquareFoot = 43560;
        //Imperial Volume
        public const decimal FluidOunce = 1;
        public const decimal GillToFluidOunce = 5;
        public const decimal PintToFluidOunce = 20;
        public const decimal QuartToFluidOunce = 40;
        public const decimal GallonToFluidOunce = 160;
        //Imperial Mass
        public const decimal GrainToPound = (decimal)1/7000;
        public const decimal DrachimToPound = (decimal)1/256;
        public const decimal OunceToPound = (decimal)1/16;
        public const decimal Pound = 1;
        public const decimal StoneToPound = 14;
        public const decimal HundredWeightToPound = 112;
        public const decimal ImperialTonToPound = 2240; 
    }
}