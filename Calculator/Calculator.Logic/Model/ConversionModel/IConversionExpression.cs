using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic.Model.ConversionModel
{
    public interface IConversionExpression
    {
        IConversionExpression Parent { get; }
        bool HasParent { get; }
        void Accept(IConversionExpressionVisitor visitor);
        void ReplaceChild(IConversionExpression oldChild, IConversionExpression newChild);
    }
}
