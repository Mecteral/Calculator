using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Model
{
    public abstract class ATrignometricFunction : AnExpressionWithValue
    {
        public override decimal Value { get; set; }
        public override string ToString() => $"{Value}";
        public override void ReplaceChild(IExpression oldChild, IExpression newChild)
        {
            throw new InvalidOperationException();
        }
    }
}
