using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic.Model.ConversionModel
{
    public abstract class AnConversionExpression : IConversionExpressionWithValue
    {
        public IConversionExpression Parent { get; protected internal set; }
        public bool HasParent => null != Parent;
        public abstract void Accept(IConversionExpressionVisitor visitor);
        public abstract void ReplaceChild(IConversionExpression oldChild, IConversionExpression newChild);
        public abstract decimal Value { get; set; }
    }
}
