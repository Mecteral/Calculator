using System.Collections.Generic;
using Calculator.Logic.Model;

namespace Calculator.Logic
{
    public class ParenthesesNode
    {
        public readonly ParenthesedExpression ParenthesedExpression = new ParenthesedExpression();
        public IList<ParenthesesNode> Children { get; } = new List<ParenthesesNode>();
        public IList<IExpression> Expressions { get; set; } = new List<IExpression>();
        public ParenthesesNode Parent { get; set; }
        public bool HasChild { get; private set; }

        public void AddChild(ParenthesesNode child)
        {
            Children.Add(child);
            child.Parent = this;
            HasChild = true;
        }
    }
}