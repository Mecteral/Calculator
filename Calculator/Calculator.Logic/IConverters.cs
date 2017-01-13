using Calculator.Logic.Model.ConversionModel;

namespace Calculator.Logic
{
    public interface IConverters {
        IConversionExpression Convert(IConversionExpression expression);
    }
}