namespace ImperialAndMetricConverter
{
    public interface IUnitConverter
    {
        IConversionExpressionWithValue Convert(IConversionExpression expression, bool toMetric);
    }
}