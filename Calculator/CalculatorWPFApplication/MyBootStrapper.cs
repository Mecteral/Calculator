using System;
using System.Windows;
using Autofac;
using Calculator.Logic;
using Calculator.Logic.Parsing.CalculationTokenizer;
using CalculatorWPFViewModels;
using Caliburn.Micro;

namespace CalculatorWPFApplication
{
    public class MyBootStrapper : BootstrapperBase
    {
        readonly IContainer mContainer;
        readonly IJSonSerializer mSerializer;

        public MyBootStrapper()
        {
            Initialize();
            mContainer = WireUpApplication();
            
            mSerializer = new JSonSerializer(new WpfApplicationStatics());
        }

        static IContainer WireUpApplication()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(ShellViewModel).Assembly)
                .Where(t => t.Name.EndsWith("ViewModel"))
                .AsSelf()
                .SingleInstance();
            builder.RegisterType<WpfApplicationStatics>().SingleInstance();
            builder.RegisterType<InputStringValidator>().SingleInstance();
            builder.RegisterType<JSonSerializer>().As<IJSonSerializer>().SingleInstance();
            builder.RegisterType<WindowManager>().AsImplementedInterfaces().AsSelf().SingleInstance();
            builder.RegisterAssemblyModules(typeof(ContainerModule).Assembly);
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            return builder.Build();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            mSerializer.Read();
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
                DefaultSubNamespaceForViewModels = "CalculatorWPFViewModels",
                DefaultSubNamespaceForViews = "CalculatorWPFApplication.Views",
                IncludeViewSuffixInViewModelNames = false
            };
            ViewLocator.ConfigureTypeMappings(cfg);
            ViewModelLocator.ConfigureTypeMappings(cfg);
        }
    }
}