using System;

namespace ImperialAndMetricConverter
{
    public class AnArithmeticConversionOperation : AnConversionExpression, IArithmeticConversionOperation
    {
        IConversionExpression mLeft;
        IConversionExpression mRight;
        public IConversionExpression Left
        {
            get { return mLeft; }
            set
            {
                mLeft = value;
                mLeft.Parent(this);
            }
        }
        public IConversionExpression Right
        {
            get { return mRight; }
            set
            {
                mRight = value;
                mRight.Parent(this);
            }
        }

        public override void Accept(IConversionExpressionVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override void ReplaceChild(IConversionExpression oldChild, IConversionExpression newChild)
        {
            if (ReferenceEquals(oldChild, mLeft))
            {
                oldChild.Parent(null);
                Left = newChild;
            }
            else if (ReferenceEquals(oldChild, mRight))
            {
                oldChild.Parent(null);
                Right = newChild;
            }
            else throw new ArgumentException();
        }

        public override decimal Value { get; set; }
    }
}
