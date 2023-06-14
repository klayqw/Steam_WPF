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
    private double balance;
    public double Balance
    {
        get => balance;
        set => base.PropertyChange(out balance, value);
    }
    private string avatarurl;
    public string Avatarurl
    {
        get => avatarurl;
        set => base.PropertyChange(out avatarurl, value);
    }
    private readonly IMessenger messenger;

    private Command tostore;
    public Command ToStore
    {
        get => new Command(() => ToStoreСommand());
        set => base.PropertyChange(out tostore, value);
    }

    private Command toaddcard;
    public Command Toaddcard
    {
        get => new Command(() => ToAddCardCommand());
        set => base.PropertyChange(out toaddcard, value);
    }

    private void ToStoreСommand()
    {
        this.messenger.Send(new GetCurrentUser(currentUser));
        this.messenger.Send(new ViewNavigate(typeof(StoreViewModel)));
    }

    private void ToAddCardCommand()
    {
        this.messenger.Send(new GetCurrentUser(currentUser));
        this.messenger.Send(new ViewNavigate(typeof(AddCardVM)));
    }

    public SettingViewVm(IMessenger messenger)
    {
        this.messenger = messenger;
        messenger.Subscribe<GetCurrentUser>((message) =>
        {
            if (message is GetCurrentUser user)
            {
                currentUser = user.User;
                Avatarurl = currentUser.AvatarUrl;
                Balance = currentUser.Card.Balance;
            }
        });

    }
}
