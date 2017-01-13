using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic.Model.ConversionModel
{
    static class ExpressionExtension
    {
            public static void Parent(this IConversionExpression self, IConversionExpression newParent)
                => ((AnConversionExpression)self).Parent = newParent;

    }
}
