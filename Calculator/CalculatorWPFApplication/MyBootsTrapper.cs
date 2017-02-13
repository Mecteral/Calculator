using System;
using System.Windows;
using Autofac;
using CalculatorWPFApplication.ViewModels;
using Caliburn.Micro;

namespace CalculatorWPFApplication
{
    public class MyBootsTrapper : BootstrapperBase
    {
        readonly IContainer mContainer;
        public MyBootsTrapper()
        {
            Initialize();
            mContainer = WireUpApplication();
        }
        static IContainer WireUpApplication()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(MyBootsTrapper).Assembly)
                .Where(t => t.Name.EndsWith("ViewModel"))
                .AsSelf()
                .SingleInstance();
            builder.RegisterType<WindowManager>().AsImplementedInterfaces().AsSelf().SingleInstance();
            return builder.Build();
        }
        protected override void Configure()
        {
            LogManager.GetLog = t => new DebugLog(t);
            var cfg = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViewModels = "CalculatorWPFApplication.ViewModels",
                DefaultSubNamespaceForViews = "CalculatorWPFApplication.Views",
                IncludeViewSuffixInViewModelNames = false
            };
            ViewLocator.ConfigureTypeMappings(cfg);
            ViewModelLocator.ConfigureTypeMappings(cfg);
        }
        protected override object GetInstance(Type service, string key)
        {
            return mContainer.Resolve(service);
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}