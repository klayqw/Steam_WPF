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

namespace Steam.ViewModel;

public class SettingViewVm : ViewModelBase
{
    private User currentUser { get; set; }
    
    private readonly IMessenger messenger;

    private Command toaddcard;
    public Command Toaddcard
    {
        get => new Command(() => ToAddCardCommand());
        set => base.PropertyChange(out toaddcard, value);
    }

    private Command toeditprofile;
    public Command Toeditprofile
    {
        get => new Command(() => ToProfileUpdate());
        set => base.PropertyChange(out toaddcard, value);
    }

    private Command toupdatebalance;
    public Command Toupdatebalance
    {
        get => new Command(() => ToUpdateBalance());
        set => base.PropertyChange(out toaddcard, value);
    }
    private void ToAddCardCommand()
    {
        this.messenger.Send(new GetCurrentUser(currentUser));
        this.messenger.Send(new ViewNavigate(typeof(AddCardVM)));
    }

    private void ToUpdateBalance()
    {
        this.messenger.Send(new GetCurrentUser(currentUser));
        this.messenger.Send(new ViewNavigate(typeof(AddBalanceVM)));
    }

    private void ToProfileUpdate()
    {
        this.messenger.Send(new GetCurrentUser(currentUser));
        this.messenger.Send(new ViewNavigate(typeof(EditProfilVM)));
    }

    public SettingViewVm(IMessenger messenger)
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
