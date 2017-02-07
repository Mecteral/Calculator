using System.Collections.Generic;

namespace ImperialAndMetricConverter
{
    public class ConversionModelBuilder : IConversionTokenVisitor, IConversionModelBuilder
    {
        IConversionExpression mCurrent;
        IArithmeticConversionOperation mCurrentOperation;
        IConversionExpression mResult;

        public void Visit(ImperialLengthToken imperialLengthToken)
        {
            mCurrent = new ImperialLengthExpression {Value = imperialLengthToken.Value};
            if (mCurrentOperation != null)
                FillOperation();
        }

        public void Visit(MetricLengthToken metricLengthToken)
        {
            mCurrent = new MetricLengthExpression {Value = metricLengthToken.Value};
            if (mCurrentOperation != null)
                FillOperation();
        }

        public void Visit(ConversionOperatorToken conversionOperatorToken)
        {
            CreateOperatorExpression(conversionOperatorToken);
            FillOperation();
        }

        public void Visit(ImperialAreaToken imperialAreaToken)
        {
            mCurrent = new ImperialAreaExpression {Value = imperialAreaToken.Value};
            if (mCurrentOperation != null)
                FillOperation();
        }

        public void Visit(ImperialMassToken imperialMassToken)
        {
            mCurrent = new ImperialMassExpression {Value = imperialMassToken.Value};
            if (mCurrentOperation != null)
                FillOperation();
        }

        public void Visit(ImperialVolumeToken imperialVolumeToken)
        {
            mCurrent = new ImperialVolumeExpression {Value = imperialVolumeToken.Value};
            if (mCurrentOperation != null)
                FillOperation();
        }

        public void Visit(MetricAreaToken metricAreaToken)
        {
            mCurrent = new MetricAreaExpression {Value = metricAreaToken.Value};
            if (mCurrentOperation != null)
                FillOperation();
        }

        public void Visit(MetricMassToken metricMassToken)
        {
            mCurrent = new MetricMassExpression {Value = metricMassToken.Value};
            if (mCurrentOperation != null)
                FillOperation();
        }

        public void Visit(MetricVolumeToken metricVolumeToken)
        {
            mCurrent = new MetricVolumeExpression {Value = metricVolumeToken.Value};
            if (mCurrentOperation != null)
                FillOperation();
        }

        public IConversionExpression BuildFrom(IEnumerable<IConversionToken> tokens)
        {
            foreach (var conversionToken in tokens)
            {
                conversionToken.Accept(this);
            }
            mResult = mCurrent;
            return mResult;
        }

        void CreateOperatorExpression(ConversionOperatorToken operatorToken)
        {
            switch (operatorToken.Operator)
            {
                case Operator.Add:
                    mCurrentOperation = new ConversionAddition();
                    break;
                case Operator.Subtract:
                    mCurrentOperation = new ConversionSubtraction();
                    break;
                    ;
                case Operator.Multiply:
                    mCurrentOperation = new ConversionMultiplication();
                    break;
                case Operator.Divide:
                    mCurrentOperation = new ConversionDivision();
                    break;
            }
        }

        void FillOperation()
        {
            if (mCurrentOperation.Left == null)
                mCurrentOperation.Left = mCurrent;
            else
                mCurrentOperation.Right = mCurrent;
            mCurrent = mCurrentOperation;
        }
    }
}