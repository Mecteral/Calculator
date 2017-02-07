using System.Data;

namespace ImperialAndMetricConverter
{
    public class MetricToImperialConverter : IMetricToImperialConverter
    {
        public IConversionExpressionWithValue Convert(IConversionExpression expression)
        {
            if (expression is MetricLengthExpression)
            {
                var tempExpression = (MetricLengthExpression) expression;
                return new ImperialLengthExpression {Value = tempExpression.Value * (decimal) 3.280839895 };
            }
            else if (expression is MetricAreaExpression)
            {
                var tempExpression = (MetricAreaExpression)expression;
                return new ImperialAreaExpression { Value = tempExpression.Value * (decimal) 10.76391041671 };
            }
            else if (expression is MetricMassExpression)
            {
                var tempExpression = (MetricMassExpression)expression;
                return new ImperialMassExpression { Value = tempExpression.Value * (decimal) 0.00220462262185 };
            }
            else if (expression is MetricVolumeExpression)
            {
                var tempExpression = (MetricVolumeExpression)expression;
                return new ImperialVolumeExpression { Value = tempExpression.Value * (decimal) 33.8140226 };
            }
            else
            {
                throw new InvalidExpressionException();
            }
        }
    }
}
