using System;
using Caliburn.Micro;
using Autofac;
using CalculatorWPFViewModels;

namespace CalculatorWPFApplication
{
    public class MyBootStrapper : BootstrapperBase
    {
        readonly IContainer mContainer;
        public MyBootStrapper()
        {
            Initialize();
            mContainer = WireUpApplication();
        }
        static IContainer WireUpApplication()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(ShellViewModel).Assembly)
                .Where(t => t.Name.EndsWith("ViewModel"))
                .AsSelf()
                .SingleInstance();
            builder.RegisterType<WindowManager>().AsImplementedInterfaces().AsSelf().SingleInstance();
            builder.RegisterAssemblyModules(typeof(Calculator.Logic.ContainerModule).Assembly);
            return builder.Build();
        }
        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
        protected override object GetInstance(Type service, string key)
        {
            return mContainer.Resolve(service);
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