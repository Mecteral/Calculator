using System;
using System.Data;
using Calculator.Logic.Conversion;
using Calculator.Logic.Model.ConversionModel;
using Calculator.Logic.Parsing.ConversionTokenizer;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Conversion
{
    [TestFixture]
    public class UnitConverterTests
    {
        UnitConverter mUnderTest;
        Func<bool, IConverters> mConverterFactory;
        IImperialToMetricConverter mImperial;
        IMetricToImperialConverter mMetric;


        [SetUp]
        public void SetUp()
        {
        }

        void FillFactory(bool toMetric)
        {
            mImperial = Substitute.For<IImperialToMetricConverter>();
            mMetric = Substitute.For<IMetricToImperialConverter>();
            mConverterFactory = b => toMetric ? (IConverters) mImperial : mMetric;
        }

        [Test]
        public void Visitor_Not_Necessary()
        {
            FillFactory(true);
            mUnderTest = new UnitConverter(mConverterFactory);
            var expression = Substitute.For<IConversionExpressionWithValue>();
            mUnderTest.Convert(expression, true);
        }s
        void ConversionVariaton<TOperation, TFirst, TSecond, TResult>(bool toMetric) where TOperation : IArithmeticConversionOperation, new() where TFirst : IConversionExpressionWithValue, new() where TSecond : IConversionExpressionWithValue, new() where TResult : IConversionExpressionWithValue, new()
        {
            var expression = new TOperation {Left = new TFirst() {Value = 20M}, Right = new TSecond() {Value = 20M}};
            FillFactory(toMetric);
            mUnderTest = new UnitConverter(mConverterFactory);
            var result = new TResult() {Value = 20M};
            if (toMetric)
                mImperial.Convert(Arg.Any<IConversionExpressionWithValue>()).Returns(result);
            else
                mMetric.Convert(Arg.Any<IConversionExpressionWithValue>()).Returns(result);
            mUnderTest.Convert(expression, toMetric);
        }

        [Test]
        public void Double_Imperial_To_Metric_Area()
        {
            ConversionVariaton<ConversionAddition, ImperialAreaExpression, ImperialAreaExpression, MetricAreaExpression>(true);
        }
        [Test]
        public void Double_Imperial_To_Metric_Length()
        {
            ConversionVariaton<ConversionAddition,ImperialLengthExpression,ImperialLengthExpression,MetricLengthExpression>(true);
        }
        [Test]
        public void Double_Imperial_To_Metric_Volume()
        {
            ConversionVariaton<ConversionDivision, ImperialVolumeExpression, ImperialVolumeExpression, MetricVolumeExpression>(true);
        }
        [Test]
        public void Double_Imperial_To_Metric_Mass()
        {
            ConversionVariaton<ConversionMultiplication, ImperialMassExpression, ImperialMassExpression, MetricMassExpression>(true);
        }
        [Test]
        public void Double_Metric_To_Imperial_Mass()
        {
            ConversionVariaton<ConversionMultiplication, MetricMassExpression, MetricMassExpression, ImperialMassExpression>(false);
        }
        [Test]
        public void Double_Metric_To_Imperial_Length()
        {
            ConversionVariaton<ConversionSubtraction, MetricLengthExpression, MetricLengthExpression, ImperialLengthExpression>(false);
        }
        [Test]
        public void Double_Metric_To_Imperial_Volume()
        {
            ConversionVariaton<ConversionMultiplication, MetricVolumeExpression, MetricVolumeExpression, ImperialVolumeExpression>(false);
        }
        [Test]
        public void Double_Metric_To_Imperial_Area()
        {
            ConversionVariaton<ConversionMultiplication, MetricAreaExpression, MetricAreaExpression, ImperialAreaExpression>(false);
        }
        [Test]
        public void Single_Metric_To_Imperial_Area()
        {
            ConversionVariaton<ConversionMultiplication, ImperialAreaExpression, MetricAreaExpression, ImperialAreaExpression>(false);
        }
        [Test]
        public void Single_Metric_To_Imperial_Mass()
        {
            ConversionVariaton<ConversionMultiplication, ImperialMassExpression, MetricMassExpression, ImperialMassExpression>(false);
        }
        [Test]
        public void Single_Metric_To_Imperial_Length()
        {
            ConversionVariaton<ConversionSubtraction, ImperialLengthExpression, MetricLengthExpression, ImperialLengthExpression>(false);
        }
        [Test]
        public void Single_Metric_To_Imperial_Volume()
        {
            ConversionVariaton<ConversionMultiplication, ImperialVolumeExpression, MetricVolumeExpression, ImperialVolumeExpression>(false);
        }
        [Test]
        public void Single_Imperial_To_Metric_Area()
        {
            ConversionVariaton<ConversionMultiplication, MetricAreaExpression, ImperialAreaExpression, ImperialAreaExpression>(false);
        }
        [Test]
        public void Single_Imperial_To_Metric_Mass()
        {
            ConversionVariaton<ConversionMultiplication, MetricMassExpression, ImperialMassExpression, ImperialMassExpression>(false);
        }
        [Test]
        public void Single_Imperial_To_Metric_Length()
        {
            ConversionVariaton<ConversionSubtraction, MetricLengthExpression, ImperialLengthExpression, ImperialLengthExpression>(false);
        }
        [Test]
        public void Single_Imperial_To_Metric_Volume()
        {
            ConversionVariaton<ConversionMultiplication, MetricVolumeExpression, ImperialVolumeExpression, ImperialVolumeExpression>(false);
        }
        [Test]
        public void Double_Metric_To_Metric_Mass()
        {
            ConversionVariaton<ConversionMultiplication, MetricMassExpression, MetricMassExpression, MetricMassExpression>(true);
        }
        [Test]
        public void Double_Metric_To_Metric_Length()
        {
            ConversionVariaton<ConversionSubtraction, MetricLengthExpression, MetricLengthExpression, MetricLengthExpression>(true);
        }
        [Test]
        public void Double_Metric_To_Metric_Volume()
        {
            ConversionVariaton<ConversionMultiplication, MetricVolumeExpression, MetricVolumeExpression, MetricVolumeExpression>(true);
        }
        [Test]
        public void Double_Metric_To_Metric_Area()
        {
            ConversionVariaton<ConversionMultiplication, MetricAreaExpression, MetricAreaExpression, MetricAreaExpression>(true);
        }
        [Test]
        public void Double_Imperial_To_Imperial_Mass()
        {
            ConversionVariaton<ConversionMultiplication, ImperialMassExpression, ImperialMassExpression, ImperialMassExpression>(false);
        }
        [Test]
        public void Double_Imperial_To_Imperial_Length()
        {
            ConversionVariaton<ConversionSubtraction, ImperialLengthExpression, ImperialLengthExpression, ImperialLengthExpression>(false);
        }
        [Test]
        public void Double_Imperial_To_Imperial_Volume()
        {
            ConversionVariaton<ConversionMultiplication, ImperialVolumeExpression, ImperialVolumeExpression, ImperialVolumeExpression>(false);
        }
        [Test]
        public void Double_Imperial_To_Imperial_Area()
        {
            ConversionVariaton<ConversionMultiplication, ImperialAreaExpression, ImperialAreaExpression, ImperialAreaExpression>(false);
        }
    }
}
