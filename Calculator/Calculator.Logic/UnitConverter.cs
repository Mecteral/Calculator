using System.Data;
using System.Text;
using Calculator.Logic.Model.ConversionModel;

namespace Calculator.Logic
{
    public class UnitConverter : IConversionExpressionVisitor
    {
        IConverters mConverter;
        IConversionExpression mResult;
        IConversionExpression mReplacement;
        bool mToMetric;

        public void Visit(ConversionAddition conversionAddition)
        {
            VisitOperands(conversionAddition);
            Calculate(conversionAddition);
        }

        public void Visit(ConversionDivision conversionDivision)
        {
            VisitOperands(conversionDivision);
            Calculate(conversionDivision);
        }

        public void Visit(ConversionSubtraction conversionSubtraction)
        {
            VisitOperands(conversionSubtraction);
            Calculate(conversionSubtraction);
        }

        public void Visit(ConversionMultiplication conversionMultiplication)
        {
            VisitOperands(conversionMultiplication);
            Calculate(conversionMultiplication);
        }

        public void Visit(MetricVolumeExpression metricVolumeExpression) {}

        public void Visit(ImperialAreaExpression imperialAreaExpression) {}

        public void Visit(ImperialLengthExpression imperialLengthExpression) {}

        public void Visit(ImperialMassExpression imperialMassExpression) {}

        public void Visit(ImperialVolumeExpression imperialVolumeExpression) {}

        public void Visit(MetricAreaExpression metricAreaExpression) {}

        public void Visit(MetricLengthExpression metricLengthExpression) {}

        public void Visit(MetricMassExpression metricMassExpression) {}

        public IConversionExpression Convert(IConversionExpression expression, bool toMetric)
        {
            mToMetric = toMetric;
            CreateConverter(toMetric);
            if (!CheckIfVisitorIsNecessary(expression))
                return ConvertSingleExpression(expression);
            expression.Accept(this);
            return mResult;
        }

        void CreateConverter(bool toMetric)
        {
            if (toMetric)
                mConverter = new ImperialToMetricConverter();
            else
                mConverter = new MetricToImperialConverter();
        }

        static bool CheckIfVisitorIsNecessary(IConversionExpression expression)
        {
            return expression is AnArithmeticConversionOperation;
        }

        void Calculate(IArithmeticConversionOperation operation)
        {
            if (operation.Left.GetType() != operation.Right.GetType())
            {
                CreateReplacement(operation);
            }
            if (operation.HasParent)
                {
                    operation.Parent.ReplaceChild(operation, mReplacement);
                }
                else
                {
                    mResult = mReplacement;
                }
        }

        void VisitOperands(IArithmeticConversionOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }

        void CreateReplacement(IArithmeticConversionOperation operation)
        {
            var lhs =  operation.Left;
            var rhs =  operation.Right;
            if (lhs is MetricVolumeExpression && rhs is ImperialVolumeExpression)
            {
                if (mToMetric)
                {
                    var templhs = (MetricVolumeExpression)ConvertSingleExpression(rhs);
                    var temprhs = (MetricVolumeExpression) lhs;
                    mReplacement = new MetricVolumeExpression {Value = templhs.Value+temprhs.Value};
                }
                else
                {
                    var templhs = (ImperialVolumeExpression)ConvertSingleExpression(lhs);
                    var temprhs = (ImperialVolumeExpression)rhs;
                    mReplacement = new ImperialVolumeExpression { Value = templhs.Value + temprhs.Value };
                }

            }
            else if (lhs is MetricAreaExpression && rhs is ImperialAreaExpression)
            {
                if (mToMetric)
                {
                    var templhs = (MetricAreaExpression)ConvertSingleExpression(rhs);
                    var temprhs = (MetricAreaExpression)lhs;
                    mReplacement = new MetricAreaExpression { Value = templhs.Value + temprhs.Value };
                }
                else
                {
                    var templhs = (ImperialAreaExpression)ConvertSingleExpression(lhs);
                    var temprhs = (ImperialAreaExpression)rhs;
                    mReplacement = new ImperialAreaExpression { Value = templhs.Value + temprhs.Value };
                }
            }
            else if (lhs is MetricLengthExpression && rhs is ImperialLengthExpression)
            {
                if (mToMetric)
                {
                    var templhs = (MetricLengthExpression)ConvertSingleExpression(rhs);
                    var temprhs = (MetricLengthExpression)lhs;
                    mReplacement = new MetricLengthExpression { Value = templhs.Value + temprhs.Value };
                }
                else
                {
                    var templhs = (ImperialLengthExpression)ConvertSingleExpression(lhs);
                    var temprhs = (ImperialLengthExpression)rhs;
                    mReplacement = new ImperialLengthExpression { Value = templhs.Value + temprhs.Value };
                }
            }
            else if (lhs is MetricMassExpression && rhs is ImperialMassExpression)
            {
                if (mToMetric)
                {
                    var templhs = (MetricMassExpression)ConvertSingleExpression(rhs);
                    var temprhs = (MetricMassExpression)lhs;
                    mReplacement = new MetricMassExpression { Value = templhs.Value + temprhs.Value };
                }
                else
                {
                    var templhs = (ImperialMassExpression)ConvertSingleExpression(lhs);
                    var temprhs = (ImperialMassExpression)rhs;
                    mReplacement = new ImperialMassExpression { Value = templhs.Value + temprhs.Value };
                }
            }
            else if (lhs is ImperialVolumeExpression && rhs is MetricVolumeExpression)
            {
                if (mToMetric)
                {
                    var templhs = (MetricVolumeExpression)ConvertSingleExpression(lhs);
                    var temprhs = (MetricVolumeExpression)rhs;
                    mReplacement = new MetricVolumeExpression { Value = templhs.Value + temprhs.Value };
                }
                else
                {
                    var templhs = (ImperialVolumeExpression)ConvertSingleExpression(rhs);
                    var temprhs = (ImperialVolumeExpression)lhs;
                    mReplacement = new ImperialVolumeExpression { Value = templhs.Value + temprhs.Value };
                }
            }
            else if (lhs is ImperialAreaExpression && rhs is MetricAreaExpression)
            {
                if (mToMetric)
                {
                    var templhs = (MetricAreaExpression)ConvertSingleExpression(lhs);
                    var temprhs = (MetricAreaExpression)rhs;
                    mReplacement = new MetricAreaExpression { Value = templhs.Value + temprhs.Value };
                }
                else
                {
                    var templhs = (ImperialAreaExpression)ConvertSingleExpression(rhs);
                    var temprhs = (ImperialAreaExpression)lhs;
                    mReplacement = new ImperialAreaExpression { Value = templhs.Value + temprhs.Value };
                }
            }
            else if (lhs is ImperialLengthExpression && rhs is MetricLengthExpression)
            {
                if (mToMetric)
                {
                    var templhs = (MetricLengthExpression)ConvertSingleExpression(lhs);
                    var temprhs = (MetricLengthExpression)rhs;
                    mReplacement = new MetricLengthExpression { Value = templhs.Value + temprhs.Value };
                }
                else
                {
                    var templhs = (ImperialLengthExpression)ConvertSingleExpression(rhs);
                    var temprhs = (ImperialLengthExpression)lhs;
                    mReplacement = new ImperialLengthExpression { Value = templhs.Value + temprhs.Value };
                }
            }
            else if (lhs is ImperialMassExpression && rhs is MetricMassExpression)
            {
                if (mToMetric)
                {
                    var templhs = (MetricMassExpression)ConvertSingleExpression(lhs);
                    var temprhs = (MetricMassExpression)rhs;
                    mReplacement = new MetricMassExpression { Value = templhs.Value + temprhs.Value };
                }
                else
                {
                    var templhs = (ImperialMassExpression)ConvertSingleExpression(rhs);
                    var temprhs = (ImperialMassExpression)lhs;
                    mReplacement = new ImperialMassExpression { Value = templhs.Value + temprhs.Value };
                }
            }
            else
            {
                throw new InvalidExpressionException();
            }
        }

        IConversionExpression ConvertSingleExpression(IConversionExpression expression)
        {
            return mConverter.Convert(expression);
        }
    }
}