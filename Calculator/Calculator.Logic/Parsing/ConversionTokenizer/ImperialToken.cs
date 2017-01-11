using System;
using Calculator.Logic.Parsing.CalculationTokenizer;
using System.Globalization;

namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public class ImperialToken : IToken
    {
        public decimal Value { get; private set; }
        decimal mNumber;
        string mUnit;
        string mNumberAsText = null;

        public ImperialToken(string asText)
        {
            asText = asText.Replace(',', '.');

            foreach (var c in asText)
            {
                if (char.IsNumber(c) || c == '.')
                {
                    ConvertToFeetIfPossible();
                    mNumberAsText += c;
                }
                else if (char.IsLetter(c))
                {
                    ParseIfPossible();
                    mUnit += c;
                }
            }
        }

        void ConvertToFeetIfPossible()
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
        private void ConvertToMetersAndAddToValue()
        {
            switch (mUnit)
            {
                case "in":
                    Value += mNumber * 12;
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
            mNumber = 0;
            mUnit = null;
        }

        public void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
