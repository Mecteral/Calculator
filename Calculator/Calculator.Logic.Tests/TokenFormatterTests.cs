using Calculator.Logic.Parsing;
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
            var input = "(2+3)*4/5-6.7";
            var tokenizer = new Tokenizer();
            tokenizer.Tokenize(input);
            var underTest = new TokenFormatter();
            var result = underTest.Format(tokenizer.Tokens);
            result.Should().Be("(2+3)*4/5-6.7");
        }
    }
}