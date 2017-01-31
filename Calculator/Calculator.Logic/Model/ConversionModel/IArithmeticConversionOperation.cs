using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic.Model.ConversionModel
{
    public interface IArithmeticConversionOperation : IConversionExpression
    {
        IConversionExpression Left { get; set; }
        IConversionExpression Right { get; set; }
    }
}
