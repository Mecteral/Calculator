using System;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.Conversion;
using Calculator.Logic.Model.ConversionModel;
using Calculator.Logic.Parsing.ConversionTokenizer;

namespace Calculator.Logic.Facades
{
    public class ConversionFacade : IConversionFacade
    {
        static ApplicationArguments sArgs;
        readonly IReadableOutputCreator mReadOutputCreator;
        readonly IUnitConverter mUnitConverter;
        readonly IConversionTokenizer mConversionTokenizer;
        readonly IConversionModelBuilder mConversionModelBuilder;
        public ConversionFacade(IReadableOutputCreator readOutputCreator, IUnitConverter unitConverter, IConversionTokenizer conversionTokenizer, IConversionModelBuilder conversionModelBuilder)
        {
            mReadOutputCreator = readOutputCreator;
            mUnitConverter = unitConverter;
            mConversionTokenizer = conversionTokenizer;
            mConversionModelBuilder = conversionModelBuilder;
        }

        public string ConvertUnits(string input, ApplicationArguments args)
        {
            sArgs = args;
            Console.WriteLine("Do you want to convert to the metric system?");
            var userInput = Console.ReadLine();
            var toMetric = userInput == "y";
            mConversionTokenizer.Tokenize(input, sArgs);
            var converted = UseUnitConverter(CreateConversionInMemoryModel(mConversionTokenizer), toMetric);
            return mReadOutputCreator.MakeReadable((IConversionExpressionWithValue)converted);
        }
        IConversionExpression CreateConversionInMemoryModel(IConversionTokenizer token) => mConversionModelBuilder.BuildFrom(token.Tokens);

        IConversionExpression UseUnitConverter(IConversionExpression expression, bool toMetric) => mUnitConverter.Convert(expression, toMetric);
    }
}
