using System;
using System.Collections.Generic;

namespace Calculator.Model
{
    /// <summary>
    /// Abstract Class for all Operators ( Multiply , Divide , Minus , Plus , Power)
    /// </summary>
    public abstract class AnArithmeticOperation : AnExpression, IArithmeticOperation
    {
        IExpression mLeft;
        IExpression mRight;
        public IExpression Left
        {
            get { return mLeft; }
            set
            {
                mLeft = value;
                mLeft.Parent(this);
            }
        }
        public IExpression Right
        {
            get { return mRight; }
            set
            {
                mRight = value;
                mRight.Parent(this);
            }
        }
        public override void ReplaceChild(IExpression oldChild, IExpression newChild)
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
        public override IEnumerable<IExpression> Children => new[] {mLeft, Right};
    }
}