using System;
using System.Globalization;
using System.Linq;

namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    public abstract class AnTrigonometricToken
    {
        double mNumber;
        readonly bool mToRadiant = true;

        protected AnTrigonometricToken(string input)
        {
            input = input.Replace(',', '.');
            if (input.Contains("rad"))
                mToRadiant = true;
            else if (input.Contains("deg"))
                mToRadiant = false;
            mNumber = double.Parse(ExtractNumber(input), NumberStyles.Any, CultureInfo.InvariantCulture);
            ConvertToDegreeIfNeeded();
            UseFunctions(input);
        }

        public decimal Value { get; private set; }

        void ConvertToDegreeIfNeeded()
        {
            if (!mToRadiant)
                mNumber = mNumber * (Math.PI / 180);
        }

        void UseFunctions(string input)
        {
            var radians = mNumber*(Math.PI/180);
            if (input.Contains("cos"))
            {
                Value = (decimal) Math.Cos(mNumber);
            }
            else if (input.Contains("sin"))
            {
                Value = (decimal) Math.Sin(mNumber);
            }
            else if (input.Contains("tan"))
            {
                Value = (decimal) Math.Tan(mNumber);
            }
        }

        static string ExtractNumber(string input)
        {
            return input.Where(c => char.IsNumber(c) || c == '.').Aggregate("", (current, c) => current + c);
        }
    }
}