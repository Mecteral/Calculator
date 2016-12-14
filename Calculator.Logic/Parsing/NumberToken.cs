using System.Globalization;

namespace Calculator.Logic.Parsing
{
    /// <summary>
    /// Takes in a Number and creates a Token, changes commas to dots
    /// </summary>
    public class NumberToken : IToken
    {
        public NumberToken(string asText)
        {
            asText = asText.Replace(',', '.');
            Value = double.Parse(asText, NumberStyles.Any, CultureInfo.InvariantCulture);
        }

        public double Value { get; }
        public void Accept(ITokenVisitor visitor) { visitor.Visit(this); }
    }
}