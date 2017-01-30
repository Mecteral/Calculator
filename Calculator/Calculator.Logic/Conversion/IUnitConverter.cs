using Calculator.Logic.Model.ConversionModel;

namespace Calculator.Logic.Conversion
{
    public interface IUnitConverter
    {
        IConversionExpressionWithValue Convert(IConversionExpression expression, bool toMetric);
    }
}