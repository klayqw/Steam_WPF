using Steam.Messages;
using Steam.Messages.Base;
using Steam.Service;
using Steam.Service.Base;
using Steam.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Steam.ViewModel;

public class LoginRegistorVM : ViewModelBase
{
    private string login;
    private string password;
    public string Login
    {
        get => login;
        set => base.PropertyChange(out login, value);
    }
    public string Password
    {
        get => password;
        set => base.PropertyChange(out password, value);
    }
    private readonly IMessenger messenger;
    private Visibility welcomeVisibility;
    private Visibility errorvisibility;

    public Visibility WelcomeVisibility
    {
        get { return welcomeVisibility; }
        set { base.PropertyChange(out welcomeVisibility, value); }
    }
    public Visibility ErrorVisibility
    {
        get { return errorvisibility; }
        set { base.PropertyChange(out errorvisibility, value); }
    }

    private Command registrationView;
    public Command RegistrationView
    {
        get => new Command(() => this.messenger.Send(new ViewNavigate(typeof(RegistrationVM))));
        set => base.PropertyChange(out registrationView, value);
    }

    private Command mainview;
    public Command Mainview
    {
        get => new Command(() => this.messenger.Send(new ViewNavigate(typeof(MainWVM))));
        set => base.PropertyChange(out registrationView, value);
    }

    private Command log;
    public Command Log
    {
        get => new Command(() => LoginCommand());
        set => base.PropertyChange(out log, value);
    }

    private void LoginCommand()
    {
        ErrorVisibility = Visibility.Hidden;
        WelcomeVisibility = Visibility.Hidden;
        var login = Login;
        var password = Password;
        if (App.ServiceContainer.GetInstance<EntityFramework>().Users.Any(x => x.Login == login && x.Password == password))
        {
            WelcomeVisibility = Visibility.Visible;
            Mainview.Execute(null);
            return;
        }
        ErrorVisibility = Visibility.Visible;
    }

    public LoginRegistorVM(IMessenger messenger)
    {
        ErrorVisibility = Visibility.Hidden;
        WelcomeVisibility = Visibility.Hidden;
        this.messenger = messenger;
    }

}
