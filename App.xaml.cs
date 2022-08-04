using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
            ServiceProvider = ConfigureServices();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public static new App Current => (App)Application.Current;

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            ServiceProvider provider =
                services.AddSingleton<MainWindow>()
                .AddSingleton<MainWindowViewModel>()
                .AddSingleton<IDataService, XmlDataService>()
                .AddSingleton<IOrderService, MockOrderService>()
                .BuildServiceProvider();
            return provider;
        }

        protected override void OnStartup(StartupEventArgs args)
        {
            MainWindow wnd= ServiceProvider.GetRequiredService<MainWindow>();
            wnd.Show();
            base.OnStartup(args);
        }
    }
}
