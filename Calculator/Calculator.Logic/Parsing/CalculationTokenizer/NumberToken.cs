using System.Globalization;

namespace Calculator.Logic.Parsing.CalculationTokenizer
{
    /// <summary>
    /// Takes in a Number and creates a Token, changes commas to dots
    /// </summary>
    public class NumberToken : IToken
    {
        public NumberToken(string asText)
        {
            asText = asText.Replace(',', '.');
            Value = decimal.Parse(asText, NumberStyles.Any, CultureInfo.InvariantCulture);
        }
        public decimal Value { get; }
        public void Accept(ITokenVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}