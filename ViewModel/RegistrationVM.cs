using Steam.Messages;
using Steam.Models;
using Steam.Service;
using Steam.Service.Base;
using Steam.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Steam.ViewModel;

public class RegistrationVM : ViewModelBase
{
    private bool emailsend = false;
    private readonly IMessenger messenger;
    private Visibility errorvisibility;
    private Visibility codevisibility;
    private PasswordBox passwordbox;
    private string loginget;
    private string passwordget;
    private string emailget;
    private int emailcode;
    private int codeget;

    public int CodeGet
    {
        get => codeget;
        set => base.PropertyChange(out  codeget, value);
        
    }

    public string LoginGet
    {
        get => loginget;
        set => base.PropertyChange(out loginget, value);
    }

    public string Passwordget
    {
        get => loginget;
        set => base.PropertyChange(out passwordget, value);
    }

    public string Emailget
    {
        get => emailget;
        set => base.PropertyChange(out emailget, value);
    }

    public Visibility Codevisibility
    {
        get { return codevisibility; }
        set { base.PropertyChange(out codevisibility, value); }
    }
    public Visibility ErrorVisibility
    {
        get { return errorvisibility; }
        set { base.PropertyChange(out errorvisibility, value); }
    }

    private Command returntologin;
    public Command Returntologin
    {
        get => new Command(() => this.messenger.Send(new ViewNavigate(typeof(LoginRegistorVM))));
        set => base.PropertyChange(out returntologin, value);
    }

    private Command registration;
    public Command Registation
    {
        get => new Command(() => Registration());
        set => base.PropertyChange(out registration, value);
    }


    private void Registration()
    {
        this.Codevisibility = Visibility.Hidden;
        this.ErrorVisibility = Visibility.Hidden;
        if (App.ServiceContainer.GetInstance<EntityFramework>().Users.Any(x => x.Login == LoginGet) || App.ServiceContainer.GetInstance<EntityFramework>().Users.Any(x => x.Email == Emailget))
        {
            this.ErrorVisibility = Visibility.Visible;
            return;
        }
        if (Passwordget.Length <= 0 || LoginGet.Length <= 0)
        {
            this.ErrorVisibility = Visibility.Visible;
            return;
        }
        if (Passwordget.Length >= 20 || LoginGet.Length >= 15)
        {
            this.ErrorVisibility = Visibility.Visible;
            return;
        }        
        if (emailsend == false)
        {
            emailcode = App.ServiceContainer.GetInstance<EmailSend>().SendCodeEmail(this.emailget);
            this.Codevisibility = Visibility.Visible;
            emailsend = true;
            return;
        }
        if(emailsend == true)
        {
            if (CodeGet != emailcode)
            {
                this.ErrorVisibility = Visibility.Visible;
                return;
            }
        }
        var user = new User()
        {
            Login = LoginGet,
            Password = Passwordget,
            Email = Emailget,
            AvatarUrl = "https://avatars.mds.yandex.net/i?id=78ac0a5c7ec9284a50f0a6e3c158a9ee4ef86afa-7543369-images-thumbs&n=13",
            Nickname = LoginGet,

        };
        try
        {
            App.ServiceContainer.GetInstance<EntityFramework>().Users.Add(user);
            App.ServiceContainer.GetInstance<EntityFramework>().SaveChanges();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return;
        }
        MessageBox.Show("All Done");
        Returntologin.Execute(null);
    }

    public RegistrationVM(IMessenger mess)
    {
        ErrorVisibility = Visibility.Hidden;
        Codevisibility = Visibility.Hidden;
        this.messenger = mess;
    }

}
