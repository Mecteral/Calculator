using Calculator.Logic.Parsing.CalculationTokenizer;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class TokenFormatterTests
    {
        [Test]
        public void TokenFormatter_Converts_Tokens_Into_Proper_Signs()
        {
            var input = "(2+3)*4/5-6.7+cos(60deg)+sin(30deg)+tan(45deg)+2a^2+sqrt(16)";
            var tokenizer = new Tokenizer();
            tokenizer.Tokenize(input, null);
            var underTest = new TokenFormatter();
            var result = underTest.Format(tokenizer.Tokens);
            result.Should().Be("(2+3)*4/5-6.7+0.5+0.5+1.0+2*a^2+16^0.5");
        }
    }
}