using System;
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
            Value += mNumber * mMap[mUnit];
            mUnit = null;
            mNumber = 0;
        }
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
            {UnitAbbreviations.Drachim, ConversionFactors.DrachimToPound},
            {UnitAbbreviations.Ounce, ConversionFactors.OunceToPound},
            {UnitAbbreviations.Pound, ConversionFactors.MultiplicationByOne},
            {UnitAbbreviations.Stone, ConversionFactors.StoneToPound},
            {UnitAbbreviations.HundredWeight, ConversionFactors.HundredWeightToPound},
            {UnitAbbreviations.ImperialTon, ConversionFactors.ImperialTonToPound}
        };
    }

    public class UnitAbbreviations
    {
        //Metric Length
        public const string Millimeters = "mm";
        public const string Centimeters = "cm";
        public const string Meters = "m";
        public const string Kilometers = "km";
        //Metric Mass
        public const string Milligram = "mg";
        public const string Gram = "g";
        public const string Kilogram = "kg";
        public const string Ton = "t";
        //Metric Volume
        public const string Milliliters = "ml";
        public const string Centiliters = "cl";
        public const string Liters = "l";
        public const string Hectoliters = "hl";
        //Metric Area
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
        public const string Drachim = "dr";
        public const string Ounce = "oz";
        public const string Pound = "lb";
        public const string Stone = "st";
        public const string HundredWeight = "cwt";
        public const string ImperialTon = "it";
    }

    public class ConversionFactors
    {
        //Metric
        public const decimal MetricDivisionOneMillion = (decimal)1.0E-6;
        public const decimal MetricDivisionOneThousand = (decimal)1.0E-3;
        public const decimal MetricDivisionOneHundred = (decimal)1.0E-2;
        public const decimal MultiplicationByOne = 1;
        public const decimal MetricMultiplicationOneHundred = (decimal)1.0E2;
        public const decimal MetricMultiplicationOneThousand = (decimal)1.0E3;
        public const decimal MetricMultiplicationOneMillion = (decimal)1.0E6;
        public const decimal MetricMultiplicationMeterToha = (decimal)1.0E4;
        //Imperial Length
        public const decimal ThouToFeet = (decimal)1/12000;
        public const decimal InchToFeet = (decimal)1/12;
        public const decimal YardToFeet = 3;
        public const decimal ChainToFeet = 66;
        public const decimal FurlongToFeet = 660;
        public const decimal MileToFeet = 5280;
        public const decimal LeagueToFeet = 15840;
        public const decimal FathomToFeet = 608001;
        //Imperial Area
        public const decimal PerchToSquareFoot = (decimal) 272.25;
        public const decimal RoodToSquareFoot = 10890;
        public const decimal AcreToSquareFoot = 43560;
        //Imperial Volume
        public const decimal GillToFluidOunce = 5;
        public const decimal PintToFluidOunce = 20;
        public const decimal QuartToFluidOunce = 40;
        public const decimal GallonToFluidOunce = 160;
        //Imperial Mass
        public const decimal GrainToPound = (decimal)1/7000;
        public const decimal DrachimToPound = (decimal)1/256;
        public const decimal OunceToPound = (decimal)1/16;
        public const decimal StoneToPound = 14;
        public const decimal HundredWeightToPound = 112;
        public const decimal ImperialTonToPound = 2240; 
    }
}