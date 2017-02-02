using System;
using System.Globalization;
using System.Linq;
using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    public abstract class AnTrigonometricToken
    {
        double mNumber;
        bool mToDegree;
        readonly ApplicationArguments mArgs;

        protected AnTrigonometricToken(string input, ApplicationArguments args)
        {
            mArgs = args;
            input = input.Replace(',', '.');
            SetConversionIfSpecified(input);
            mNumber = double.Parse(ExtractNumber(input), NumberStyles.Any, CultureInfo.InvariantCulture);
            ConvertToDegreeIfNeeded();
            UseFunctions(input);
        }

        public decimal Value { get; private set; }

        void ConvertToDegreeIfNeeded()
        {
            if (mToDegree)
                mNumber = mNumber * (Math.PI / 180);
        }

        void UseFunctions(string input)
        {
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

        void SetConversionIfSpecified(string input)
        {
            if (mArgs != null)
            {
                mToDegree = mArgs.ToDegree;
            }
            if (input.Contains("rad"))
                mToDegree = false;
            else if (input.Contains("deg"))
                mToDegree = true;
        }
        static string ExtractNumber(string input)
        {
            return input.Where(c => char.IsNumber(c) || c == '.').Aggregate("", (current, c) => current + c);
        }
    }
}