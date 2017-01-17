using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Model
{
    public class CosineExpression : AnExpressionWithValue
    {
        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void ReplaceChild(IExpression oldChild, IExpression newChild)
        {
            throw new System.NotImplementedException();
        }
    }
}
