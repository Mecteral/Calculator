using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic.Model.ConversionModel
{
    public interface IConversionExpressionWithValue : IConversionExpression
    {
        decimal Value { get; set; }
    }
}
