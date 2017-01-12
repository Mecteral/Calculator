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
                    Value += mNumber * (decimal)0.001;
                    break;
                case "cm":
                    Value += mNumber * (decimal)0.01;
                    break;
                case "m":
                    Value += mNumber;
                    break;
                case "km":
                    Value += mNumber * 1000;
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
                case "mI":
                    Value += mNumber * 5280;
                    break;
            }
            mUnit = null;
            mNumber = 0;
        }
    }
}