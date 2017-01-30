using Calculator.Logic.Model.ConversionModel;

namespace Calculator.Logic.Conversion
{
    public interface IUnitConverter
    {
        IConversionExpression Convert(IConversionExpression expression, bool toMetric);
    }
}