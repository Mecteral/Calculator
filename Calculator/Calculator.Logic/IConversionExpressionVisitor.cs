namespace ImperialAndMetricConverter
{
    public interface IConversionExpressionVisitor
    {
        void Visit(ConversionAddition conversionAddition);
        void Visit(ConversionDivision conversionDivision);
        void Visit(ConversionSubtraction conversionSubtraction);
        void Visit(ConversionMultiplication conversionMultiplication);
        void Visit(MetricVolumeExpression metricVolumeExpression);
        void Visit(ImperialAreaExpression imperialAreaExpression);
        void Visit(ImperialLengthExpression imperialLengthExpression);
        void Visit(ImperialMassExpression imperialMassExpression);
        void Visit(ImperialVolumeExpression imperialVolumeExpression);
        void Visit(MetricAreaExpression metricAreaExpression);
        void Visit(MetricLengthExpression metricLengthExpression);
        void Visit(MetricMassExpression metricMassExpression);
    }
}