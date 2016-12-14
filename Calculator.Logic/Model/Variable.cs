using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Logic.Model
{
    public class Variable : IExpression
    {
        public double Value { get; set; }
        public string Variables { get; set; }
        public void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }
}
