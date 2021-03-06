﻿using System;
using System.Windows;
using Autofac;
using Calculator.Logic;
using Calculator.Logic.Parsing.CalculationTokenizer;
using Calculator.Logic.WpfApplicationProperties;
using Calculator.WPF.ViewModels;
using Caliburn.Micro;

namespace CalculatorWPFApplication
{
    
    public class MyBootStrapper : BootstrapperBase
    {
        readonly IContainer mContainer;
        readonly IAllSerializableSettings mSettings;
        public MyBootStrapper()
        {
            Initialize();
            mSettings = SettingsSerializer.Read();
            mContainer = WireUpApplication();
        }

        IContainer WireUpApplication()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterInstance(mSettings).AsImplementedInterfaces();
            builder.RegisterType<InputStringValidator>().SingleInstance();
            builder.RegisterType<WindowManager>().AsImplementedInterfaces().AsSelf().SingleInstance();
            builder.RegisterAssemblyModules(typeof(LogicModule).Assembly);
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<ConfigurationOptionTabViewModel>().AsSelf().As<IMainScreenTabItem>().SingleInstance();
            builder.RegisterType<ConfigurationThemeTabViewModel>().AsSelf().As<IMainScreenTabItem>().SingleInstance();
            builder.RegisterType<ConfigurationViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<ConfigurationWindowViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<InputViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<ShellViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<ConversionViewModel>().AsSelf().As<IUnitsAndAbbreviationsSource>().SingleInstance();
            return builder.Build();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            Application.Current.Resources.Source = new Uri(mSettings.UsedWpfTheme,
                UriKind.RelativeOrAbsolute);
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return mContainer.Resolve(service);
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            SettingsSerializer.Write(mSettings);
            base.OnExit(sender, e);
        }

        protected override void Configure()
        {
            LogManager.GetLog = t => new DebugLog(t);
            var cfg = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViewModels = "Calculator.WPF.ViewModels",
                DefaultSubNamespaceForViews = "CalculatorWPFApplication.Views",
                IncludeViewSuffixInViewModelNames = false
            };
            ViewLocator.ConfigureTypeMappings(cfg);
            ViewModelLocator.ConfigureTypeMappings(cfg);
        }
    }
}