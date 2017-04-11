using System.Collections.Generic;
using ModernRonin.PraeterArtem.Functional;

namespace Calculator.Model
{
    public abstract class AnExpressionWithName : AnExpression, IExpressionWithName
    {
        public virtual string Name { get; set; }
        public sealed override IEnumerable<IExpression> Children => Null.Enumerable<IExpression>();
    }
}