using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Model
{
    public abstract class AnExpressionWithValue : AnExpression, IExpressionWithValue
    {
        public virtual decimal Value { get; set; }
    }
}
