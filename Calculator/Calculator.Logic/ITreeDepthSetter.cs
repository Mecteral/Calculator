using Calculator.Model;

namespace Calculator.Logic
{
    public interface ITreeDepthSetter
    {
        void SetTreeDepth(IExpression expression);
    }
}