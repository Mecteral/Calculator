﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Parsing.CalculationTokenizer;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Calculator.Logic.Parsing.ConversionTokenizer;
using FluentAssertions;

namespace Calculator.Logic.Tests.Parsing.ConversionTokenizer
{
    [TestFixture]
    public class MetricLengthTokenTests
    {
        static void CheckWithNumber(string input, int position, decimal expected)
        {
            var converter = new Logic.Parsing.ConversionTokenizer.ConversionTokenizer();
            converter.Tokenize(input, null);
            var underTest = converter.Tokens;
            var result = (MetricLengthToken)underTest.ElementAt(position);
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
            CheckWithNumber("1000m20cm", 0, 1000.20M);
        }

        [Test]
        public void MetricUnitsWithAddition()
        {
            var converter = new Logic.Parsing.ConversionTokenizer.ConversionTokenizer();
            converter.Tokenize("10m+20.3km", null);
            var underTest = converter.Tokens;
            underTest.ElementAt(0).Should().BeOfType<MetricLengthToken>().Which.Value.Should().Be(10);
            underTest.ElementAt(1).Should().BeOfType<ConversionOperatorToken>().Which.Operator.Should().Be(Operator.Add);
            underTest.ElementAt(2).Should().BeOfType<MetricLengthToken>().Which.Value.Should().Be(20300);
        }
    }
}