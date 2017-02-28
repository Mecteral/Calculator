﻿using System;
using Autofac;
using Calculator.Logic.Facades;
using Calculator.Logic.Model;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Logic.Pipelines;
using Calculator.Logic.Simplifying;
using Calculator.Logic.Utilities;
using Mecteral.UnitConversion;

namespace Calculator.Logic
{
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ModelBuilder>().As<IModelBuilder>();
            builder.RegisterType<Tokenizer>().As<ITokenizer>();
            builder.RegisterType<ConversionTokenizer>().As<IConversionTokenizer>();
            builder.RegisterType<ConversionModelBuilder>().As<IConversionModelBuilder>();
            builder.RegisterType<FormattingExpressionVisitor>().As<IExpressionFormatter>();
            builder.RegisterType<PipelineEvaluator>().As<IPipelineEvaluator>();
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
            builder.RegisterType<DirectCalculationSimplifier>().As<IDirectCalculationSimplifier>().As<ISimplifier>();
            builder.RegisterType<ParenthesesSimplifier>().As<IParenthesesSimplifier>().As<ISimplifier>();
            builder.RegisterType<AdditionAndSubtractionMover>().As<IAdditionAndSubtractionMover>().As<ISimplifier>();
            builder.RegisterType<VariableCalculator>().As<IVariableCalculator>().As<ISimplifier>();
            builder.RegisterType<ExpressionEqualityChecker>().As<IExpressionEqualityChecker>();
            builder.RegisterType<Simplifier>().As<ISimplify>();
            builder.RegisterType<EvaluatingExpressionVisitor>().As<IExpressionEvaluator>();
        }
    }
}