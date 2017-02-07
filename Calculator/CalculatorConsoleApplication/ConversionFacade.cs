using System;

namespace ImperialAndMetricConverter
{
    public class ConversionFacade
    {
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
        public string ConvertUnits(string input, bool toMetric)
        {
            mConversionTokenizer.Tokenize(input);
            var converted = UseUnitConverter(CreateConversionInMemoryModel(mConversionTokenizer), toMetric);
            return mReadableOutputCreator.MakeReadable(converted);
        }

        public static string Convert(string input, bool toMetric)
        {
            Func<bool, IConverters> conversionFactory = b => b ? (IConverters)new ImperialToMetricConverter() : new MetricToImperialConverter();
            var tokenizer = new ConversionTokenizer();
            tokenizer.Tokenize(input);
            var modelBuilder = new ConversionModelBuilder();
            var model = modelBuilder.BuildFrom(tokenizer.Tokens);
            var converter = new UnitConverter(conversionFactory);
            var converted = converter.Convert(model, toMetric);
            var readableOutputCreator = new ReadableOutputCreator();
            return readableOutputCreator.MakeReadable(converted);
        }

        IConversionExpression CreateConversionInMemoryModel(IConversionTokenizer token) => mConversionModelBuilder.BuildFrom(token.Tokens);

        IConversionExpressionWithValue UseUnitConverter(IConversionExpression expression, bool toMetric) => mUnitConverter.Convert(expression, toMetric);
    }
}
