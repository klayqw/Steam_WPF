using SimpleInjector;
using Steam.Service;
using Steam.Service.Base;
using Steam.ViewModel;
using Steam.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Steam;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static Container ServiceContainer { get; set; } = new Container();


    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        ConfigureContainer();

        StartWindow<LoginRegistorVM>();
    }

    private void StartWindow<T>() where T : ViewModelBase
    {
        var startView = new MainWindow();

        var startViewModel = ServiceContainer.GetInstance<MainVM>();
        startViewModel.ActiveViewModel = ServiceContainer.GetInstance<T>();
        startView.DataContext = startViewModel;

        startView.ShowDialog();
    }

    private void ConfigureContainer()
    {
        ServiceContainer.RegisterSingleton<IMessenger, Messenger>();

        ServiceContainer.RegisterSingleton<LoginRegistorVM>();
        ServiceContainer.RegisterSingleton<RegistrationVM>();
        ServiceContainer.RegisterSingleton<MainVM>();

        ServiceContainer.Verify();
    }
}
