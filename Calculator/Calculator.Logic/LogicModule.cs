using System;
using Autofac;
using Calculator.Logic.ArgumentParsing;
using Calculator.Logic.ConfigFile;
using Calculator.Logic.Evaluation;
using Calculator.Logic.Facades;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Logic.Pipelines;
using Calculator.Logic.Simplifying;
using Calculator.Logic.Utilities;
using Mecteral.UnitConversion;

namespace Calculator.Logic
{
    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ModelBuilder>().As<IModelBuilder>();
            builder.RegisterType<Tokenizer>().As<ITokenizer>();
            builder.RegisterType<ConversionTokenizer>().As<IConversionTokenizer>();
            builder.RegisterType<ConversionModelBuilder>().As<IConversionModelBuilder>();
            builder.RegisterType<FormattingExpressionVisitor>().As<IExpressionFormatter>();
            builder.RegisterType<EvaluationPipeline>().As<IEvaluationPipeline>();
            builder.RegisterType<ReadableOutputCreator>().As<IReadableOutputCreator>();
            builder.RegisterType<ConversionFacade>().As<IConversionFacade>();
            builder.RegisterType<UnitConverter>()
                .As<IUnitConverter>()
                .WithParameter((parameter, context) => parameter.ParameterType == typeof(Func<bool, IConverters>),
                    (parameter, context) =>
                    {
                        var cc = context.Resolve<IComponentContext>();
                        Func<bool, IConverters> result =
                            toMetric =>
                                toMetric
                                    ? (IConverters)cc.Resolve<IImperialToMetricConverter>()
                                    : cc.Resolve<IMetricToImperialConverter>();
                        return result;
                    });
            builder.RegisterType<ImperialToMetricConverter>().As<IImperialToMetricConverter>();
            builder.RegisterType<MetricToImperialConverter>().As<IMetricToImperialConverter>();
            builder.RegisterType<SimplificationPipeline>().As<ISimplificationPipeline>();
            builder.RegisterType<EvaluationFacade>().As<IEvaluationFacade>();
            builder.RegisterType<SymbolicSimplificationFacade>().As<ISymbolicSimplificationFacade>();

            builder.RegisterType<ExpressionsWithOnlyConstantChildrenSimplifier>().As<ISimplifier>().InstancePerDependency();
            builder.RegisterType<ParenthesisAroundConstantsRemovingSimplifier>().As<ISimplifier>().InstancePerDependency();
            builder.RegisterType<AdditionAndSubtractionMover>().As<IAdditionAndSubtractionMover>().As<ISimplifier>().InstancePerDependency();
            builder.RegisterType<VariableCalculator>().As<IVariableCalculator>().As<ISimplifier>().InstancePerDependency();
            builder.RegisterType<NeutralElementEliminatingSimplifier>().As<ISimplifier>().InstancePerDependency();
            builder.RegisterType<MultiplicationByZeroRemovingSimplifier>().As<ISimplifier>().InstancePerDependency();
            builder.RegisterType<DistributeLawSimplifier>().As<ISimplifier>().InstancePerDependency();
            builder.RegisterType<AggregateSimplifier>().As<IAggregateSimplifier>();

            builder.RegisterType<ExpressionEqualityChecker>().As<IExpressionEqualityChecker>();
            builder.RegisterType<EvaluatingExpressionVisitor>().As<IExpressionEvaluator>();
            builder.RegisterType<WpfCalculationExecutor>().As<IWpfCalculationExecutor>();
            builder.RegisterType<ApplicationArguments>().As<IApplicationArguments>().SingleInstance();
            builder.RegisterType<ConsoleToMetricDecider>().As<IConsoleToMetricDecider>();
            builder.RegisterType<ConfigFileWriter>().As<IConfigFileWriter>();
            builder.RegisterType<TreeDepthSetter>().As<ITreeDepthSetter>();
            builder.RegisterType<ParenthesesCounter>().As<IParenthesesCounter>();
            builder.RegisterType<AdditiveCounter>().As<IAdditiveCounter>();
            builder.RegisterType<TreeDepthCounter>().As<ITreeDepthCounter>();
            builder.RegisterType<ExpressionCounter>().As<IExpressionCounter>();
            builder.RegisterType<AggregateEvaluator>().As<IAggregateEvaluator>();
        }
    }
}