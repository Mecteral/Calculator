using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Logic.Model.ConversionModel;
using Calculator.Logic.Parsing.ConversionTokenizer;
using FluentAssertions;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Calculator.Logic.Tests
{
    [TestFixture]
    public class UnitConverterTests
    {
        static IConversionExpression Convert(string input, bool toMetric)
        {
            var tokens = new ConversionTokenizer();
            tokens.Tokenize(input, null);
            var model = new ConversionModelBuilder();
            var converter = new UnitConverter();
            return converter.Convert(model.BuildFrom(tokens.Tokens), toMetric);
        }

        [Test]
        public void SimpleAdditionGetsCalculatedCorrectly()
        {
            var result = Convert("21ft+20m", true);
            result.Should().BeOfType<MetricLengthExpression>().Subject.Value.Should().Be((decimal) 26.4008);
        }

        [Test]
        public void DoubleFootCalculation()
        {
            var result = Convert("21ft+20ft", true);
            result.Should().BeOfType<MetricLengthExpression>().Which.Value.Should().Be((decimal) 12.4968);
        }

        [Test]
        public void TripleFootCalculationWithoutConversion()
        {
            var result = Convert("21ft+20ft+1ft", false);
            result.Should().BeOfType<ImperialLengthExpression>().Which.Value.Should().Be(42);
        }

        [Test]
        public void DoubleFootSubtraction()
        {
            var result = Convert("21ft-20ft", false);
            result.Should().BeOfType<ImperialLengthExpression>().Which.Value.Should().Be(1);
        }

        [Test]
        public void DifferentSystemsThrowException()
        {
            var tokens = new ConversionTokenizer();
            tokens.Tokenize("10sft+10ft", null);
            var model = new ConversionModelBuilder();
            var converter = new UnitConverter();
            Action a= () => converter.Convert(model.BuildFrom(tokens.Tokens), false);

            a.ShouldThrow<InvalidExpressionException>();
        }

        [Test]
        public void ConvertSingleExpression()
        {
            var result = Convert("1ml", false);
            result.Should().BeOfType<ImperialVolumeExpression>().Which.Value.Should().Be((decimal) 0.0338140226);
        }
        [Test]
        public void ImperialAreaToMetricArea()
        {
            var result = Convert("13sft+1qmm",true);
            result.Should().BeOfType<MetricAreaExpression>().Which.Value.Should().Be((decimal) 1.207701);
        }
        [Test]
        public void ImperialAreaToMetricArea2()
        {
            var result = Convert("13perch+1qcm", true);
            result.Should().BeOfType<MetricAreaExpression>().Which.Value.Should().Be((decimal) 328.797325);
        }
        [Test]
        public void ImperialAreaToMetricArea3()
        {
            var result = Convert("13rood+1qkm", true);
            result.Should().BeOfType<MetricAreaExpression>().Which.Value.Should().Be((decimal)1013151.8530);
        }
        [Test]
        public void ImperialAreaToMetricArea4()
        {
            var result = Convert("13acre+1ha", true);
            result.Should().BeOfType<MetricAreaExpression>().Which.Value.Should().Be((decimal)62607.4120);
        }

        [Test]
        public void ImperialVolumeToMetricVolume()
        {
            var result = Convert("13floz+1ml", true);
            result.Should().BeOfType<MetricVolumeExpression>().Which.Value.Should().Be((decimal)0.3703698125);
        }
        [Test]
        public void DoubleImperialMetricVolume()
        {
            var result = Convert("13floz+13floz", true);
            result.Should().BeOfType<MetricVolumeExpression>().Which.Value.Should().Be((decimal)0.738739625);
        }
        [Test]
        public void DoubleImperialMetricMass()
        {
            var result = Convert("13oz+13oz", true);
            result.Should().BeOfType<MetricMassExpression>().Which.Value.Should().Be((decimal)737.08760125);
        }
        [Test]
        public void DoubleMetricToImperialMass()
        {
            var result = Convert("13g+13g", false);
            result.Should().BeOfType<ImperialMassExpression>().Which.Value.Should().Be((decimal)0.05732018816810);
        }
        [Test]
        public void DoubleMetricToImperialArea()
        {
            var result = Convert("13qm+13qm", false);
            result.Should().BeOfType<ImperialAreaExpression>().Which.Value.Should().Be((decimal)279.86167083446);
        }
        [Test]
        public void DoubleMetricToImperialLength()
        {
            var result = Convert("13m+13m", false);
            result.Should().BeOfType<ImperialLengthExpression>().Which.Value.Should().Be((decimal)85.301837270);
        }
        [Test]
        public void DoubleImperialMetricArea()
        {
            var result = Convert("13sft+13sft", true);
            result.Should().BeOfType<MetricAreaExpression>().Which.Value.Should().Be((decimal)2.4154);
        }
        [Test]
        public void DoubleMetricToImperialVolume()
        {
            var result = Convert("13l+13l", false);
            result.Should().BeOfType<ImperialVolumeExpression>().Which.Value.Should().Be((decimal)879.1645876);
        }
        [Test]
        public void MetricToimperialVolume()
        {
            var result = Convert("13floz+1ml", false);
            result.Should().BeOfType<ImperialVolumeExpression>().Which.Value.Should().Be((decimal)13.0338140226);
        }
        [Test]
        public void MetricToimperialVolume2()
        {
            var result = Convert("13l+1floz", true);
            result.Should().BeOfType<MetricVolumeExpression>().Which.Value.Should().Be((decimal)13.0284130625);
        }
        [Test]
        public void ImperialVolumeToMetricVolume2()
        {
            var result = Convert("13gi+1cl", true);
            result.Should().BeOfType<MetricVolumeExpression>().Which.Value.Should().Be((decimal)1.8568490625);
        }
        [Test]
        public void ImperialVolumeToMetricVolume3()
        {
            var result = Convert("13pt+1l", true);
            result.Should().BeOfType<MetricVolumeExpression>().Which.Value.Should().Be((decimal)8.38739625);
        }
        [Test]
        public void ImperialVolumeToMetricVolume4()
        {
            var result = Convert("13qt+1hl", true);
            result.Should().BeOfType<MetricVolumeExpression>().Which.Value.Should().Be((decimal)114.7747925);
        }
        [Test]
        public void ImperialVolumeToMetricVolume5()
        {
            var result = Convert("13gal+1l", true);
            result.Should().BeOfType<MetricVolumeExpression>().Which.Value.Should().Be((decimal)60.09917);
        }
        [Test]
        public void ImperialMassToMetricMass()
        {
            var result = Convert("0gr+13g", true);
            result.Should().BeOfType<MetricMassExpression>().Which.Value.Should().Be((decimal)13);
        }
        [Test]
        public void ImperialMassToMetricMass2()
        {
            var result = Convert("13dr+1g", true);
            result.Should().BeOfType<MetricMassExpression>().Which.Value.Should().Be((decimal)24.0339875390625);
        }
        [Test]
        public void ImperialMassToMetricMass3()
        {
            var result = Convert("13oz+1kg", true);
            result.Should().BeOfType<MetricMassExpression>().Which.Value.Should().Be((decimal)1368.543800625);
        }
        [Test]
        public void ImperialMassToMetricMass4()
        {
            var result = Convert("13lb+1t", true);
            result.Should().BeOfType<MetricMassExpression>().Which.Value.Should().Be((decimal)1005896.70081);
        }
        [Test]
        public void ImperialMassToMetricMass5()
        {
            var result = Convert("13st+1g", true);
            result.Should().BeOfType<MetricMassExpression>().Which.Value.Should().Be((decimal)82554.81134);
        }
        [Test]
        public void ImperialMassToMetricMass6()
        {
            var result = Convert("13cwt+1g", true);
            result.Should().BeOfType<MetricMassExpression>().Which.Value.Should().Be((decimal)660431.49072);
        }
        [Test]
        public void ImperialMassToMetricMass7()
        {
            var result = Convert("13it+1g", true);
            result.Should().BeOfType<MetricMassExpression>().Which.Value.Should().Be((decimal)13208610.8144);
        }
        [Test]
        public void ImperialLengthToMetricLength()
        {
            var result = Convert("13ft+1mm", true);
            result.Should().BeOfType<MetricLengthExpression>().Which.Value.Should().Be((decimal)3.9634);
        }
        [Test]
        public void ImperialLengthToMetricLength2()
        {
            var result = Convert("0in+1cm", true);
            result.Should().BeOfType<MetricLengthExpression>().Which.Value.Should().Be((decimal)0.01);
        }
        [Test]
        public void ImperialLengthToMetricLength3()
        {
            var result = Convert("13yd+1m", true);
            result.Should().BeOfType<MetricLengthExpression>().Which.Value.Should().Be((decimal)12.8872);
        }
        [Test]
        public void ImperialLengthToMetricLength4()
        {
            var result = Convert("13mI+km", true);
            result.Should().BeOfType<MetricLengthExpression>().Which.Value.Should().Be((decimal)20921.4720);
        }
        [Test]
        public void ImperialLengthToMetricLength5()
        {
            var result = Convert("0th+1m", true);
            result.Should().BeOfType<MetricLengthExpression>().Which.Value.Should().Be((decimal)1);
        }
        [Test]
        public void ImperialLengthToMetricLength6()
        {
            var result = Convert("13ch+1m", true);
            result.Should().BeOfType<MetricLengthExpression>().Which.Value.Should().Be((decimal)262.5184);
        }
        [Test]
        public void ImperialLengthToMetricLength7()
        {
            var result = Convert("13fur+1m", true);
            result.Should().BeOfType<MetricLengthExpression>().Which.Value.Should().Be((decimal)2616.184);
        }
        [Test]
        public void ImperialLengthToMetricLength8()
        {
            var result = Convert("13lea+1m", true);
            result.Should().BeOfType<MetricLengthExpression>().Which.Value.Should().Be((decimal)62765.4160);
        }
        [Test]
        public void ImperialLengthToMetricLength9()
        {
            var result = Convert("13ftm+1m", true);
            result.Should().BeOfType<MetricLengthExpression>().Which.Value.Should().Be((decimal)2409144.1624);
        }

        [Test]
        public void WithoutConversionMultiplicationMetric()
        {
            var result = Convert("13g*13g", true);
            result.Should().BeOfType<MetricMassExpression>().Which.Value.Should().Be(169);
        }
        [Test]
        public void WithoutConversionDivisionMetric()
        {
            var result = Convert("169l/13l", true);
            result.Should().BeOfType<MetricVolumeExpression>().Which.Value.Should().Be(13);
        }
        [Test]
        public void WithoutConversionMetricArea()
        {
            var result = Convert("13qm*13qm", true);
            result.Should().BeOfType<MetricAreaExpression>().Which.Value.Should().Be(169);
        }
        [Test]
        public void WithoutConversionMetricLength()
        {
            var result = Convert("13m*13m", true);
            result.Should().BeOfType<MetricLengthExpression>().Which.Value.Should().Be(169);
        }
        [Test]
        public void WithoutConversionImperialArea()
        {
            var result = Convert("13sft*13sft", false);
            result.Should().BeOfType<ImperialAreaExpression>().Which.Value.Should().Be(169);
        }
        [Test]
        public void WithoutConversionImperialVolume()
        {
            var result = Convert("13floz*13floz", false);
            result.Should().BeOfType<ImperialVolumeExpression>().Which.Value.Should().Be(169);
        }
        [Test]
        public void WithoutConversionImperialMass()
        {
            var result = Convert("13lb*13lb", false);
            result.Should().BeOfType<ImperialMassExpression>().Which.Value.Should().Be(169);
        }

        [Test]
        public void MetricToImperialMass()
        {
            var result = Convert("13kg+16oz", false);
            result.Should().BeOfType<ImperialMassExpression>().Which.Value.Should().Be((decimal)29.66009408405);
        }
        [Test]
        public void MetricToImperialArea()
        {
            var result = Convert("13qm-13sft", false);
            result.Should().BeOfType<ImperialAreaExpression>().Which.Value.Should().Be((decimal)126.93083541723);
        }
        [Test]
        public void MetricToImperialLength()
        {
            var result = Convert("13m+13ft", false);
            result.Should().BeOfType<ImperialLengthExpression>().Which.Value.Should().Be((decimal)55.650918635);
        }

    }
}
