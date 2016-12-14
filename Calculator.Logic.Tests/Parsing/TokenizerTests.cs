using System.Linq;
using Calculator.Logic.Parsing;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Parsing
{
    [TestFixture]
    public class TokenizerTests
    {
        [Test]
        public void Tokenizer_Creates_ParenthesesToken()
        {
            var underTest = " ( 2 + 3 ) ";
            var result = new Tokenizer(underTest);
            result.Tokenize();
            result.Tokens.First().Should().BeOfType<ParenthesesToken>();
        }

        [Test]
        public void Tokenizer_Creates_Numbertoken()
        {
            var underTest = " 2 + 3 ";
            var result = new Tokenizer(underTest);
            result.Tokenize();
            result.Tokens.First().Should().BeOfType<NumberToken>();
        }

        [Test]
        public void Tokenizer_Creates_Operatortoken()
        {
            var underTest = "+";
            var result = new Tokenizer(underTest);
            result.Tokenize();
            result.Tokens.First().Should().BeOfType<OperatorToken>();
        }

        [Test]
        public void Tokenizer_Deletes_Whitespace()
        {
            var underTest = " ( 2 + 3 )";
            var result = new Tokenizer(underTest);
            result.Tokenize();
            result.Tokens.ElementAt(1).Should().BeOfType<NumberToken>();
        }

        [Test]
        public void Tokenizer_Ignores_Tabs()
        {
            var underTest = " 2\t +3";
            var result = new Tokenizer(underTest);
            result.Tokenize();
            result.Tokens.Count().Should().Be(3);
        }
        [Test]
        public void Tokenizer_Ignores_Newline()
        {
            var underTest = " 2\n +3";
            var result = new Tokenizer(underTest);
            result.Tokenize();
            result.Tokens.Count().Should().Be(3);
        }

        [Test]
        public void Tokenizer_Contains_The_Correct_Amount_Of_Elements()
        {
            var underTest = " ( 2 + 3 )";
            var result = new Tokenizer(underTest);
            result.Tokenize();
            result.Tokens.Count().Should().Be(5);
        }

        [Test]
        public void Tokenizer_Creates_Numbertoken_Of_Double_Digit_Numbers()
        {
            var underTest = " 24 +3";
            var result = new Tokenizer(underTest);
            result.Tokenize();
            result.Tokens.First().Should().BeOfType<NumberToken>().Which.Value.Should().Be(24);
        }

        [Test]
        public void Tokenizer_Creates_Numbertoken_Of_Numbers_With_Dots()
        {
            var underTest = " 2.2345 +3";
            var result = new Tokenizer(underTest);
            result.Tokenize();
            result.Tokens.First().Should().BeOfType<NumberToken>().Which.Value.Should().Be(2.2345);
        }
        [Test]
        public void Tokenizer_Creates_Numbertoken_Of_Numbers_With_Commas()
        {
            var underTest = " 2,2345 +3";
            var result = new Tokenizer(underTest);
            result.Tokenize();
            result.Tokens.First().Should().BeOfType<NumberToken>().Which.Value.Should().Be(2.2345);
        }

        [Test]
        public void Tokenizer_Creates_VariableToken()
        {
            var underTest = "a";
            var result = new Tokenizer(underTest);
            result.Tokenize();
            result.Tokens.First().Should().BeOfType<VariableToken>().Which.Variable.Should().Be("a");
        }
        [Test]
        public void Tokenizer_Creates_VariableToken_With_Multiple_Variables()
        {
            var underTest = "abc";
            var result = new Tokenizer(underTest);
            result.Tokenize();
            result.Tokens.First().Should().BeOfType<VariableToken>().Which.Variable.Should().Be("abc");
        }

        [Test]
        public void Tokenizer_Alphabetezises_Variables()
        {
            var underTest = "cba";
            var result = new Tokenizer(underTest);
            result.Tokenize();
            result.Tokens.First().Should().BeOfType<VariableToken>().Which.Variable.Should().Be("abc");
        }
        [Test]
        public void Tokenizer_Creates_Correct_TokenEnumerable()
        {
            var underTest = "((1.1+22)*3.1/4-5.1)";
            var result = new Tokenizer(underTest);
            result.Tokenize();
            result.Tokens.First().Should().BeOfType<ParenthesesToken>().Which.IsOpening.Should().BeTrue();
            result.Tokens.ElementAt(1).Should().BeOfType<ParenthesesToken>().Which.IsOpening.Should().BeTrue();
            result.Tokens.ElementAt(2).Should().BeOfType<NumberToken>().Which.Value.Should().Be(1.1);
            result.Tokens.ElementAt(3).Should().BeOfType<OperatorToken>().Which.Operator.Should().Be(Operator.Add);
            result.Tokens.ElementAt(4).Should().BeOfType<NumberToken>().Which.Value.Should().Be(22);
            result.Tokens.ElementAt(5).Should().BeOfType<ParenthesesToken>().Which.IsOpening.Should().BeFalse();
            result.Tokens.ElementAt(6).Should().BeOfType<OperatorToken>().Which.Operator.Should().Be(Operator.Multiply);
            result.Tokens.ElementAt(7).Should().BeOfType<NumberToken>().Which.Value.Should().Be(3.1);
            result.Tokens.ElementAt(8).Should().BeOfType<OperatorToken>().Which.Operator.Should().Be(Operator.Divide);
            result.Tokens.ElementAt(9).Should().BeOfType<NumberToken>().Which.Value.Should().Be(4);
            result.Tokens.ElementAt(10).Should().BeOfType<OperatorToken>().Which.Operator.Should().Be(Operator.Subtract);
            result.Tokens.ElementAt(11).Should().BeOfType<NumberToken>().Which.Value.Should().Be(5.1);
            result.Tokens.ElementAt(12).Should().BeOfType<ParenthesesToken>().Which.IsOpening.Should().BeFalse();
            result.Tokens.Count().Should().Be(13);
        }
    }
}