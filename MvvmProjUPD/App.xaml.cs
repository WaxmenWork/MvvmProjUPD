using Microsoft.Extensions.DependencyInjection;
using MvvmProjUPD.Core;
using MvvmProjUPD.MVVM.Models.Repositories;
using MvvmProjUPD.MVVM.ViewModels;
using MvvmProjUPD.MVVM.Views;
using MvvmProjUPD.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MvvmProjUPD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<ShopViewModel>();
            services.AddTransient<ProfileViewModel>();

            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<MyDbContext>();
            services.AddTransient<UserRepository>();
            services.AddTransient<ProductRepository>();

            services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
