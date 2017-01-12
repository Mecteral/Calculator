using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic.Model.ConversionModel
{
    public class ImperialVolumeExpression : AnConversionExpression
    {
        public decimal Value { get; set; }
        public override void Accept(IConversionExpressionVisitor visitor) => visitor.Visit(this);
        public override string ToString() => $"{Value}";
        public override void ReplaceChild(IConversionExpression oldChild, IConversionExpression newChild)
        {
            throw new InvalidOperationException();
        }
    }
}
