using Calculator.Model;

namespace Calculator.Logic.Simplifying
{
    public class NeutralElementEliminatingSimplifier : AVisitingTraversingReplacer
    {
        protected override IExpression ReplaceMultiplication(Multiplication multiplication)
            => new NeutralElementOfMultiplicationRemover(multiplication).Transform();
        protected override IExpression ReplaceSubtraction(Subtraction subtraction)
            => new NeutralElementOfSubtractionRemover(subtraction).Transform();
        protected override IExpression ReplaceAddition(Addition addition)
            => new NeutralElementOfAdditionRemover(addition).Transform();
        protected override IExpression ReplaceDivision(Division division)
            => new NeutralElementOfDivisionRemover(division).Transform();
        
    }
}