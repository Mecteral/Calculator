using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    public class SqaureRootToken : IToken
    {
        public decimal Value { get; private set; }

        public SqaureRootToken(string input)
        {
            input = input.Replace(',', '.');
            var number = double.Parse(ExtractNumber(input), NumberStyles.Any, CultureInfo.InvariantCulture);
            Value = (decimal) Math.Sqrt(number);
        }

        static string ExtractNumber(string input)
        {
            return input.Where(c => char.IsNumber(c) || c == '.').Aggregate("", (current, c) => current + c);
        }

        public void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
