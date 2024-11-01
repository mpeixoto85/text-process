using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.IO;
using System.Windows;
using TextProcessUI.Configuration;
using TextProcessUI.Services;
using TextProcessUI.Services.Interfaces;

namespace TextProcessUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }
        public static IConfiguration Configuration { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddSingleton<ITextProcessAPIService, TextProcessAPIService>();
            var httpClientConfiguration = new HttpClientConfiguration();
            Configuration.GetSection("HttpClient").Bind(httpClientConfiguration);
            services.AddSingleton(httpClientConfiguration);
            //services.Configure<HttpClientConfiguration>();
            // Aquí puedes añadir otros servicios
        }
    }

}
