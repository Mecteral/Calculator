﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Calculator.Logic.Parsing.ConversionTokenizer;
using FluentAssertions;

namespace Calculator.Logic.Tests.Parsing.ConversionTokenizer
{
    [TestFixture]
    public class MetricTokenTests
    {
        static void CheckWithNumber(string input, int position, decimal expected)
        {
            var converter = new Logic.Parsing.ConversionTokenizer.ConversionTokenizer();
            converter.Tokenize(input);
            var underTest = converter.Tokens;
            var result = (MetricToken)underTest.ElementAt(position);
            result.Value.Should().Be(expected);
        }
        [Test]
        public void OneKilometerCenvertsToOneThousandMeters()
        {
            CheckWithNumber("1km", 0, 1000);
        }

        [Test]
        public void MillimetersToMeter()
        {
            CheckWithNumber("1000mm",0,1);
        }
        [Test]
        public void CentimetersToMeter()
        {
            CheckWithNumber("1000cm", 0, 10);
        }
        [Test]
        public void MeterStaysMeter()
        {
            CheckWithNumber("1000m", 0, 1000);
        }
    }
}
