namespace ImperialAndMetricConverter
{
    public interface IConverters
    {
        IConversionExpressionWithValue Convert(IConversionExpression expression);
    }
}