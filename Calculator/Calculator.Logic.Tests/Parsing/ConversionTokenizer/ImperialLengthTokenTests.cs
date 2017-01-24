using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Parsing.ConversionTokenizer;
using FluentAssertions;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Parsing.ConversionTokenizer
{
    [TestFixture]
    public class ImperialLengthTokenTests
    {
        static void CheckWithNumber(string input, int position, decimal expected)
        {
            var converter = new Logic.Parsing.ConversionTokenizer.ConversionTokenizer();
            converter.Tokenize(input, null);
            var underTest = converter.Tokens;
            var result = (ImperialLengthToken)underTest.ElementAt(position);
            result.Value.Should().Be(expected);
        }

        [Test]
        public void FeetToFeet()
        {
            CheckWithNumber("3ft",0,3);
        }

        [Test]
        public void MileToFeet()
        {
            CheckWithNumber("12mI",0,63360);
        }

        [Test]
        public void YardToFeet()
        {
            CheckWithNumber("38yd", 0, 114);
        }
    }
}
