namespace Calculator.Model
{
    /// <summary>
    /// Abstract Class for all Operators ( Multiply , Divide , Minus , Plus )
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
                ((AnExpression) mLeft).Parent = this;
            }
        }
        public IExpression Right
        {
            get { return mRight; }
            set
            {
                mRight = value;
                ((AnExpression) mRight).Parent = this;
            }
        }
    }
}