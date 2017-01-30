using System.Collections.Generic;
using Calculator.Logic.Conversion;
using Calculator.Logic.Model.ConversionModel;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Conversion
{
    [TestFixture]
    public class ReadableOutputCreatorTests
    {
        [SetUp]
        public void SetUp()
        {
            mUnderTest = new ReadableOutputCreator();
        }

        ReadableOutputCreator mUnderTest;

        static IEnumerable<object[]> TestCases
        {
            get
            {
                yield return new object[]
                {
                    new ImperialMassExpression {Value = 25M}, " 1 st 11 lb"
                };
                yield return new object[]
                {
                    new ImperialAreaExpression() {Value = 37M}, " 37 sft"
                };
                yield return new object[]
                {
                    new ImperialLengthExpression() {Value = 128M}, " 1 ch 20 yd 2 ft"
                };
                yield return new object[]
                {
                    new ImperialVolumeExpression() {Value = 39M}, " 1 pt 3 gi 4 floz"
                };
                yield return new object[]
                {
                    new MetricAreaExpression() {Value = 31239M}, "3.1239 ha"
                };
                yield return new object[]
                {
                    new MetricAreaExpression() {Value = 31M}, "31 qm"
                };
                yield return new object[]
                {
                    new MetricAreaExpression() {Value = 0.0000032M}, "3.2 qmm"
                };
                yield return new object[]
                {
                    new MetricAreaExpression() {Value = 0.00032M}, "3.2 qcm"
                };
                yield return new object[]
                {
                    new MetricAreaExpression() {Value = 312391231M}, "312.391231 qkm"
                };
                yield return new object[]
                {
                    new MetricLengthExpression() {Value = 4231M}, "4.231 km"
                };
                yield return new object[]
                {
                    new MetricLengthExpression() {Value = 423M}, "423 m"
                };
                yield return new object[]
                {
                    new MetricLengthExpression() {Value = 0.00004231M}, "0.04231 mm"
                };
                yield return new object[]
                {
                    new MetricLengthExpression() {Value = 0.04231M}, "4.231 cm"
                };
                yield return new object[]
                {
                    new MetricMassExpression() {Value = 6942M}, "6.942 kg"
                };
                yield return new object[]
                {
                    new MetricMassExpression() {Value = 6942456M}, "6.942456 t"
                };
                yield return new object[]
                {
                    new MetricMassExpression() {Value = 0.6942M}, "0.6942 g"
                };
                yield return new object[]
                {
                    new MetricMassExpression() {Value = 0.006942M}, "6.942 mg"
                };
                yield return new object[]
                {
                    new MetricVolumeExpression() {Value = 5432M}, "54.32 hl"
                };
                yield return new object[]
                {
                    new MetricVolumeExpression() {Value = 54M}, "54 l"
                };
                yield return new object[]
                {
                    new MetricVolumeExpression() {Value = 0.005432M}, "5.432 ml"
                };
                yield return new object[]
                {
                    new MetricVolumeExpression() {Value = 0.05432M}, "5.432 cl"
                };
            }
        }

        [Test]
        public void ReadableOuput_From_Imperial_Weight()
        {
            var underTest = new ImperialAreaExpression {Value = 37M};
            mUnderTest.MakeReadable(underTest).Should().Be(" 37 sft");
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ReadableOutput(IConversionExpressionWithValue input, string expectedOutput)
        {
            mUnderTest.MakeReadable(input).Should().Be(expectedOutput);
        }
    }
}