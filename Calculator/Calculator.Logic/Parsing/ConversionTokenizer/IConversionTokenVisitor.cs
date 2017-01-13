namespace Calculator.Logic.Parsing.ConversionTokenizer
{
    public interface IConversionTokenVisitor
    {
        void Visit(ImperialLengthToken imperialLengthToken);
        void Visit(MetricLengthToken metricLengthToken);
        void Visit(ConversionOperatorToken conversionOperatorToken);
        void Visit(ImperialAreaToken imperialAreaToken);
        void Visit(ImperialMassToken imperialMassToken);
        void Visit(ImperialVolumeToken imperialVolumeToken);
        void Visit(MetricAreaToken metricAreaToken);
        void Visit(MetricMassToken metricMassToken);
        void Visit(MetricVolumeToken metricVolumeToken);
    }
}