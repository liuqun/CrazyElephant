using Microsoft.Extensions.DependencyInjection;
using MyApp.Services;
using System;
using System.Windows;

namespace MyApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// The `IServiceProvider` instance to resolve application services.
        /// </summary>
        public IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            IServiceCollection services = CollectAllServices();

            ServiceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public static new App Current => (App)Application.Current;

        private static IServiceCollection CollectAllServices()
        {
            return new ServiceCollection()
                .AddSingleton<IDataService, XmlDataService>()
                .AddSingleton<IOrderService, MockOrderService>()
                .AddSingleton<MainWindow>()
                .AddSingleton<MainWindowViewModel>();
        }

        protected override void OnStartup(StartupEventArgs args)
        {
            MainWindow wnd = ServiceProvider.GetRequiredService<MainWindow>();
            wnd.Show();
            base.OnStartup(args);
        }
    }
}
