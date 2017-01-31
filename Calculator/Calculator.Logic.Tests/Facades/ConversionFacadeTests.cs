using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Conversion;
using Calculator.Logic.Facades;
using Calculator.Logic.Model.ConversionModel;
using Calculator.Logic.Parsing.ConversionTokenizer;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Calculator.Logic.Tests.Facades
{
    [TestFixture]
    public class ConversionFacadeTests
    {
        [SetUp]
        public void Setup()
        {
            mReadOutputCreator = Substitute.For<IReadableOutputCreator>();
            mUnitConverter = Substitute.For<IUnitConverter>();
            mConversionTokenizer = Substitute.For<IConversionTokenizer>();
            mConversionModelBuilder = Substitute.For<IConversionModelBuilder>();
            mUnderTest = new ConversionFacade(mReadOutputCreator, mUnitConverter, mConversionTokenizer,
                mConversionModelBuilder);
        }

        readonly ApplicationArguments mArgs = new ApplicationArguments {ToDegree = true, UnitForConversion = "m"};
        IReadableOutputCreator mReadOutputCreator;
        IUnitConverter mUnitConverter;
        IConversionTokenizer mConversionTokenizer;
        IConversionModelBuilder mConversionModelBuilder;
        ConversionFacade mUnderTest;

        [Test]
        public void ConversionModelBuilder_Receives_From_ConversionTokenizer()
        {
            const string testString = "2m + 3m";

            mUnderTest.ConvertUnits(testString, mArgs);

            mConversionModelBuilder.Received().BuildFrom(mConversionTokenizer.Tokens);
        }

        [Test]
        public void UnitConverter_Receives_From_ModelBuilder()
        {
            const string testString = "2qm + 3qm";
            var modelBuilderResult = new MetricAreaExpression();
            mConversionModelBuilder.BuildFrom(mConversionTokenizer.Tokens).Returns(modelBuilderResult);

            mUnderTest.ConvertUnits(testString, mArgs);

            mUnitConverter.Received().Convert(modelBuilderResult, false);
        }

        [Test]
        public void ReadableOutputCreator_Receives_From_UnitConverter()
        {
            const string testString = "2qm + 3qm";
            var convertResult = Substitute.For<IConversionExpressionWithValue>();
            mUnitConverter.Convert(Arg.Any<IConversionExpression>(), Arg.Any<bool>()).Returns(convertResult);

            mUnderTest.ConvertUnits(testString, mArgs);

            mReadOutputCreator.Received().MakeReadable(convertResult);
        }

        [Test]
        public void ConvertUnits_Passes_Input_To_Tokenizer()
        {
            const string testString = "2qm + 3qm";

            mUnderTest.ConvertUnits(testString, mArgs);

            mConversionTokenizer.Received().Tokenize(testString, mArgs);
        }

        [Test]
        public void ReadableOutputCreator_Returns_Correct_String()
        {
            const string testString = "2qm + 3qm";
            var convertResult = Substitute.For<IConversionExpressionWithValue>();
            mUnitConverter.Convert(Arg.Any<IConversionExpression>(), Arg.Any<bool>()).Returns(convertResult);
            const string readableResult = "5qm";
            mReadOutputCreator.MakeReadable(convertResult).Returns(readableResult);

            mUnderTest.ConvertUnits(testString, mArgs);

            mReadOutputCreator.MakeReadable(convertResult).Should().Be("5qm");
        }
    }
}