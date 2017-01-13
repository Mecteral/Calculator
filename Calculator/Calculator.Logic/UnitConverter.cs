using System;
using System.Data;
using Calculator.Logic.Model.ConversionModel;

namespace Calculator.Logic
{
    public class UnitConverter : IConversionExpressionVisitor
    {
        IConverters mConverter;
        IConversionExpression mReplacement;
        IConversionExpression mResult;
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
            else
            {
                CreateReplacementIfBothSidesOfTheOperationNeedToBeConvertedForMetric(operation);
                CreateReplacementIfBothSidesOfTheOperationNeedToBeConvertedForImperial(operation);
                CalculateIfNoConversionIsNeeded(operation);
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

        static decimal CalculateValueForSpecificOperationType(IArithmeticConversionOperation operation, decimal lhs,
            decimal rhs)
        {
            if (operation is ConversionAddition)
            {
                return lhs + rhs;
            }
            if (operation is ConversionSubtraction)
            {
                return lhs - rhs;
            }
            if (operation is ConversionMultiplication)
            {
                return lhs*rhs;
            }
            if (operation is ConversionDivision)
            {
                return lhs/rhs;
            }
            throw new InvalidExpressionException();
        }

        void MakeReplacementWithoutConversion<TSelf>(IArithmeticConversionOperation operation,
            IConversionExpressionWithValue lhs, IConversionExpressionWithValue rhs) where TSelf : IConversionExpressionWithValue, new()
        {
            var templhs = (TSelf)lhs;
            var temprhs = (TSelf)rhs;
            mReplacement = new TSelf
            {
                Value = CalculateValueForSpecificOperationType(operation, templhs.Value, temprhs.Value)
            };
        }
        void CalculateIfNoConversionIsNeeded(IArithmeticConversionOperation operation)
        {
            var lhs = operation.Left;
            var rhs = operation.Right;
            if (mToMetric)
            {
                if (lhs is MetricVolumeExpression)
                {
                    MakeReplacementWithoutConversion<MetricVolumeExpression>(operation,lhs as IConversionExpressionWithValue, rhs as IConversionExpressionWithValue);
                }
                else if (lhs is MetricAreaExpression)
                {
                    MakeReplacementWithoutConversion<MetricAreaExpression>(operation, lhs as IConversionExpressionWithValue, rhs as IConversionExpressionWithValue);
                }
                else if (lhs is MetricLengthExpression)
                {
                    MakeReplacementWithoutConversion<MetricLengthExpression>(operation, lhs as IConversionExpressionWithValue, rhs as IConversionExpressionWithValue);
                }
                else if (lhs is MetricMassExpression)
                {
                    MakeReplacementWithoutConversion<MetricMassExpression>(operation, lhs as IConversionExpressionWithValue, rhs as IConversionExpressionWithValue);
                }
            }
            else
            {
                if (lhs is ImperialVolumeExpression)
                {
                    MakeReplacementWithoutConversion<ImperialVolumeExpression>(operation, lhs as IConversionExpressionWithValue, rhs as IConversionExpressionWithValue);
                }
                else if (lhs is ImperialAreaExpression)
                {
                    MakeReplacementWithoutConversion<ImperialAreaExpression>(operation, lhs as IConversionExpressionWithValue, rhs as IConversionExpressionWithValue);
                }
                else if (lhs is ImperialLengthExpression)
                {
                    MakeReplacementWithoutConversion<ImperialLengthExpression>(operation, lhs as IConversionExpressionWithValue, rhs as IConversionExpressionWithValue);
                }
                else if (lhs is ImperialMassExpression)
                {
                    MakeReplacementWithoutConversion<ImperialMassExpression>(operation, lhs as IConversionExpressionWithValue, rhs as IConversionExpressionWithValue);
                }
            }
        }

        void CreateReplacementIfBothSidesOfTheOperationNeedToBeConvertedForImperial(
            IArithmeticConversionOperation operation)
        {
            var lhs = operation.Left;
            var rhs = operation.Right;
            if (!mToMetric)
            {
                if (lhs is MetricVolumeExpression)
                {
                    MakeReplacementWithDoubleConversion<ImperialVolumeExpression>(operation, lhs, rhs);
                }
                else if (lhs is MetricAreaExpression)
                {
                    MakeReplacementWithDoubleConversion<ImperialAreaExpression>(operation, lhs, rhs);
                }
                else if (lhs is MetricLengthExpression)
                {
                    MakeReplacementWithDoubleConversion<ImperialLengthExpression>(operation, lhs, rhs);
                }
                else if (lhs is MetricMassExpression)
                {
                    MakeReplacementWithDoubleConversion<ImperialMassExpression>(operation, lhs, rhs);
                }
            }
        }

        void MakeReplacementWithDoubleConversion<TSelf>(IArithmeticConversionOperation operation,
            IConversionExpression lhs, IConversionExpression rhs) where TSelf : IConversionExpressionWithValue, new()
        {
            var templhs = (TSelf)ConvertSingleExpression(rhs);
            var temprhs = (TSelf)ConvertSingleExpression(lhs);
            mReplacement = new TSelf
            {
                Value = CalculateValueForSpecificOperationType(operation, templhs.Value, temprhs.Value)
            };
        }
        void CreateReplacementIfBothSidesOfTheOperationNeedToBeConvertedForMetric(
            IArithmeticConversionOperation operation)
        {
            var lhs = operation.Left;
            var rhs = operation.Right;
            if (mToMetric)
            {
                if (lhs is ImperialVolumeExpression)
                {
                    MakeReplacementWithDoubleConversion<MetricVolumeExpression>(operation, lhs, rhs);
                }
                else if (lhs is ImperialLengthExpression)
                {
                    MakeReplacementWithDoubleConversion<MetricLengthExpression>(operation,lhs, rhs);
                }
                else if (lhs is ImperialAreaExpression)
                {
                    MakeReplacementWithDoubleConversion<MetricAreaExpression>(operation, lhs, rhs);
                }
                else if (lhs is ImperialMassExpression)
                {
                    MakeReplacementWithDoubleConversion<MetricMassExpression>(operation, lhs, rhs);
                }
            }
        }

        void MakeReplacement<TLeft, TRight>(IArithmeticConversionOperation operation, IConversionExpressionWithValue lhs,
            IConversionExpressionWithValue rhs) where TLeft : IConversionExpressionWithValue, new() where TRight : IConversionExpressionWithValue, new()
        {
            if (!mToMetric)
            {
                var templhs = (TLeft)ConvertSingleExpression(rhs);
                var temprhs = (TLeft)lhs;
                mReplacement = new TLeft
                {
                    Value = CalculateValueForSpecificOperationType(operation, templhs.Value, temprhs.Value)
                };
            }
            else
            {
                var templhs = (TRight)ConvertSingleExpression(lhs);
                var temprhs = (TRight)rhs;
                mReplacement = new TRight
                {
                    Value = CalculateValueForSpecificOperationType(operation, templhs.Value, temprhs.Value)
                };
            }
        }

        void VisitOperands(IArithmeticConversionOperation operation)
        {
            operation.Left.Accept(this);
            operation.Right.Accept(this);
        }

        void CreateReplacement(IArithmeticConversionOperation operation)
        {
            var lhs = operation.Left;
            var rhs = operation.Right;
            if (lhs is MetricVolumeExpression && rhs is ImperialVolumeExpression)
            {
                MakeReplacement<ImperialVolumeExpression, MetricVolumeExpression>(operation,rhs as IConversionExpressionWithValue, lhs as IConversionExpressionWithValue);
            }
            else if (lhs is MetricAreaExpression && rhs is ImperialAreaExpression)
            {
                MakeReplacement<ImperialAreaExpression, MetricAreaExpression>(operation, rhs as IConversionExpressionWithValue, lhs as IConversionExpressionWithValue);
            }
            else if (lhs is MetricLengthExpression && rhs is ImperialLengthExpression)
            {
                MakeReplacement<ImperialLengthExpression, MetricLengthExpression>(operation, rhs as IConversionExpressionWithValue, lhs as IConversionExpressionWithValue);
            }
            else if (lhs is MetricMassExpression && rhs is ImperialMassExpression)
            {
                MakeReplacement<ImperialMassExpression, MetricMassExpression>(operation, rhs as IConversionExpressionWithValue, lhs as IConversionExpressionWithValue);
            }
            else if (lhs is ImperialVolumeExpression && rhs is MetricVolumeExpression)
            {
                MakeReplacement<ImperialVolumeExpression,MetricVolumeExpression>(operation,lhs as IConversionExpressionWithValue, rhs as IConversionExpressionWithValue);
            }
            else if (lhs is ImperialAreaExpression && rhs is MetricAreaExpression)
            {
                MakeReplacement<ImperialAreaExpression, MetricAreaExpression>(operation, lhs as IConversionExpressionWithValue, rhs as IConversionExpressionWithValue);
            }
            else if (lhs is ImperialLengthExpression && rhs is MetricLengthExpression)
            {
                MakeReplacement<ImperialLengthExpression, MetricLengthExpression>(operation, lhs as IConversionExpressionWithValue, rhs as IConversionExpressionWithValue);
            }
            else if (lhs is ImperialMassExpression && rhs is MetricMassExpression)
            {
                MakeReplacement<ImperialMassExpression, MetricMassExpression>(operation, lhs as IConversionExpressionWithValue, rhs as IConversionExpressionWithValue);
            }
            else
            {
                throw new InvalidExpressionException("The systems cant be converted.");
            }
        }

        IConversionExpression ConvertSingleExpression(IConversionExpression expression)
        {
            return mConverter.Convert(expression);
        }
    }
}