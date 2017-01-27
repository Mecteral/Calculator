using Autofac;
using Calculator.Logic.Simplifying;

namespace CalculatorConsoleApplication.ContainerModules
{
    public class SimplificationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DirectCalculationSimplifier>().As<IDirectCalculationSimplifier>().As<ISimplifier>();
            builder.RegisterType<ParenthesesSimplifier>().As<IParenthesesSimplifier>().As<ISimplifier>();
            builder.RegisterType<AdditionAndSubtractionMover>().As<IAdditionAndSubtractionMover>().As<ISimplifier>();
            builder.RegisterType<VariableCalculator>().As<IVariableCalculator>().As<ISimplifier>();
            builder.RegisterType<Simplifier>().As<ISimplify>();
        }
    }
}