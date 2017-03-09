using System;
using System.Globalization;
using System.Linq;

namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    public class SquareRootToken : IToken
    {
        public SquareRootToken(string input)
        {
            input = input.Replace(',', '.');
            var number = double.Parse(ExtractNumber(input), NumberStyles.Any, CultureInfo.InvariantCulture);
            Value = (decimal) Math.Sqrt(number);
        }

        public decimal Value { get; private set; }

        public void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }

        static string ExtractNumber(string input)
        {
            return input.Where(c => char.IsNumber(c) || c == '.').Aggregate("", (current, c) => current + c);
        }
    }
}