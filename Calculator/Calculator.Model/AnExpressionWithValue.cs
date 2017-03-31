using System.Collections.Generic;
using ModernRonin.PraeterArtem.Functional;

namespace Calculator.Model
{
    public abstract class AnExpressionWithValue : AnExpression, IExpressionWithValue
    {
        public virtual decimal Value { get; set; }
        public sealed override IEnumerable<IExpression> Children => Null.Enumerable<IExpression>();
    }
}