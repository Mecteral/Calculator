namespace ImperialAndMetricConverter
{
    public interface IConversionExpressionWithValue : IConversionExpression
    {
        decimal Value { get; set; }
    }
}
