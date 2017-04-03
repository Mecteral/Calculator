using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Calculator.Logic.ArgumentParsing;

namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    public abstract class AnTrigonometricToken
    {
        double mNumber;
        bool mToDegree;
        readonly IApplicationArguments mArgs;

        protected AnTrigonometricToken(string input, IApplicationArguments args)
        {
            mArgs = args;
            input = input.Replace(',', '.');
            SetConversionIfSpecified(input);
            mNumber = double.Parse(ExtractNumber(input), NumberStyles.Any, CultureInfo.InvariantCulture);
            ConvertToDegreeIfNeeded();
            Value = CalculateValueOf(input);
        }

        public decimal Value { get; private set; }

        void ConvertToDegreeIfNeeded()
        {
            if (mToDegree)
                mNumber = mNumber * (Math.PI / 180);
        }

        decimal CalculateValueOf(string input)
        {
            var functionNameToFunction = new Dictionary<string, Func<double, double>>
            {
                {"cos", Math.Cos },
                {"sin", Math.Sin },
                {"tan", Math.Tan },
            };
            return
                functionNameToFunction.Where(mapping => input.Contains(mapping.Key))
                    .Select(mapping => (decimal) mapping.Value(mNumber))
                    .FirstOrDefault();
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