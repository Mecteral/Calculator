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
                    Value += mNumber * 1000000;
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
                    Value += mNumber * 1000000;
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
}