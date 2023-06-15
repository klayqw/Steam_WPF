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

namespace Steam.ViewModel;

public class EditProfilVM : ViewModelBase
{
    private Command useoldnickname;
    public Command UseOldNickname
    {
        get => new Command(() => Username = currentUser.Nickname);
        set => base.PropertyChange(out  useoldnickname, value);
    }

    private Command useoldpassword;
    public Command Useoldpassword
    {
        get => new Command(() => Password = currentUser.Password);
        set => base.PropertyChange(out useoldpassword, value);
    }

    private Command useoldemail;
    public Command Useoldemail
    {
        get => new Command(() => Email = currentUser.Email);
        set => base.PropertyChange(out useoldemail, value);
    }

    private Command useoldavatarurl;
    public Command Useoldavatarurl
    {
        get => new Command(() => {
            AvatarUrl = currentUser.AvatarUrl;
        });
        set => base.PropertyChange(out useoldavatarurl, value);
    }

    private bool send = false;
    private int codefrom;
    private string username;
    public string Username
    {
        get => username;
        set => base.PropertyChange(out username, value);
    }

    private string email;
    public string Email
    {
        get => email;
        set => base.PropertyChange(out email, value);
    }
    private string avatarurl;
    public string AvatarUrl
    {
        get => avatarurl;
        set => base.PropertyChange(out avatarurl, value);
    }
    private string password;
    public string Password
    {
        get => password;
        set => base.PropertyChange(out password, value);
    }
    private string code;
    public string Code
    {
        get => code;
        set => base.PropertyChange(out code, value);
    }
    private IMessenger messenger;

    private User currentUser { get; set; }

    private Command apply;
    public Command Apply
    {
        get => new Command(() => ApplyCommand());
        set => base.PropertyChange(out  apply, value);
    }

    private void ApplyCommand()
    {
        if(Email != currentUser.Email)
        {
            if (send == false)
            {
                codefrom = App.ServiceContainer.GetInstance<EmailSend>().SendCodeEmail(currentUser.Email);
                MessageBox.Show("Send to old email");
                return;
            }
            if (codefrom != int.Parse(Code))
            {
                MessageBox.Show("Error");
                return;
            }
        }
        
        currentUser.Nickname = Username;
        currentUser.Email = Email;
        currentUser.Password = Password;
        currentUser.AvatarUrl = AvatarUrl;
        App.ServiceContainer.GetInstance<EntityFramework>().SaveChanges();
        MessageBox.Show("All done");
        App.ServiceContainer.GetInstance<EntityFramework>().SaveChanges();
        return;
    }

    public EditProfilVM(IMessenger messenger)
    {
        this.messenger = messenger;
        messenger.Subscribe<GetCurrentUser>((message) =>
        {
            if (message is GetCurrentUser user)
            {
                currentUser = user.User;
            }
        });

    }
}
