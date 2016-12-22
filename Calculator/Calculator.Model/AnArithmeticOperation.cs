namespace Calculator.Model
{
    /// <summary>
    /// Abstract Class for all Operators ( Multiply , Divide , Minus , Plus )
    /// </summary>
    public abstract class AnArithmeticOperation : IArithmeticOperation
    {
        IExpression mLeft;
        IExpression mRight;
        public IExpression Left
        {
            get { return mLeft; }
            set
            {
                mLeft = value;
                mLeft.Parent = this;
            }
        }
        public IExpression Right
        {
            get { return mRight; }
            set
            {
                mRight = value;
                mRight.Parent = this;
            }
        }
        public IExpression Parent { get; set; }
        public abstract void Accept(IExpressionVisitor visitor);
    }
}