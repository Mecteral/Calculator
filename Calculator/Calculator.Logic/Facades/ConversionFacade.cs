using System;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Conversion;
using Calculator.Logic.Model.ConversionModel;
using Calculator.Logic.Parsing.ConversionTokenizer;

namespace Calculator.Logic.Facades
{
    public class ConversionFacade : IConversionFacade
    {
        ApplicationArguments mArgs;
        readonly IReadableOutputCreator mReadableOutputCreator;
        readonly IUnitConverter mUnitConverter;
        readonly IConversionTokenizer mConversionTokenizer;
        readonly IConversionModelBuilder mConversionModelBuilder;
        public ConversionFacade(IReadableOutputCreator readOutputCreator, IUnitConverter unitConverter, IConversionTokenizer conversionTokenizer, IConversionModelBuilder conversionModelBuilder)
        {
            mReadableOutputCreator = readOutputCreator;
            mUnitConverter = unitConverter;
            mConversionTokenizer = conversionTokenizer;
            mConversionModelBuilder = conversionModelBuilder;
        }

        public string ConvertUnits(string input, ApplicationArguments args)
        {
            mArgs = args;
            Console.WriteLine("Do you want to convert to the metric system?");
            var userInput = Console.ReadLine();
            var toMetric = userInput == "y";
            mConversionTokenizer.Tokenize(input, mArgs);
            var converted = UseUnitConverter(CreateConversionInMemoryModel(mConversionTokenizer), toMetric);
            return mReadableOutputCreator.MakeReadable(converted);
        }
        IConversionExpression CreateConversionInMemoryModel(IConversionTokenizer token) => mConversionModelBuilder.BuildFrom(token.Tokens);

        IConversionExpressionWithValue UseUnitConverter(IConversionExpression expression, bool toMetric) => (IConversionExpressionWithValue)mUnitConverter.Convert(expression, toMetric);
    }
}
