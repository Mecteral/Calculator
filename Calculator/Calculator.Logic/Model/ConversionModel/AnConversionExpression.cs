using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic.Model.ConversionModel
{
    abstract class AnConversionExpression : IConversionExpression
    {
        public IConversionExpression Parent { get; protected internal set; }
        public bool HasParent => null != Parent;
        public abstract void Accept(IConversionExpressionVisitor visitor);
        public abstract void ReplaceChild(IConversionExpression oldChild, IConversionExpression newChild);
    }
}
