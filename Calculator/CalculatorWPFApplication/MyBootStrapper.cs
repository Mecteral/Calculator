using System;
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
        readonly IJSonSerializer mSerializer;
        readonly IAllSerializableSettings mSettings = new AllSerializableSettings();

        public MyBootStrapper()
        {
            Initialize();
            mContainer = WireUpApplication();
            
            mSerializer = new JSonSerializer(mSettings);
        }

        IContainer WireUpApplication()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(ShellViewModel).Assembly)
                .Where(t => t.Name.EndsWith("ViewModel"))
                .AsSelf()
                .SingleInstance();
            builder.RegisterInstance(mSettings).AsImplementedInterfaces();
            builder.RegisterType<WindowProperties>().As<IWindowProperties>().SingleInstance();
            builder.RegisterType<ConversionProperties>().As<IConversionProperties>().SingleInstance();
            builder.RegisterType<InputStringValidator>().SingleInstance();
            builder.RegisterType<JSonSerializer>().As<IJSonSerializer>().SingleInstance();
            builder.RegisterType<WindowManager>().AsImplementedInterfaces().AsSelf().SingleInstance();
            builder.RegisterAssemblyModules(typeof(LogicModule).Assembly);
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<ConfigurationOptionTabViewModel>().As<IMainScreenTabItem>().SingleInstance();
            builder.RegisterType<ConfigurationThemeTabViewModel>().As<IMainScreenTabItem>().SingleInstance();
            builder.RegisterType<ConfigurationViewModel>().SingleInstance();
            return builder.Build();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            mSerializer.Read();
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
            mSerializer.Write();
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