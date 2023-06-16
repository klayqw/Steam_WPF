using Steam.Messages;
using Steam.Models;
using Steam.Service;
using Steam.Service.Base;
using Steam.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Steam.ViewModel;

class MainVM : ViewModelBase
{
    private Visibility hood;
    public Visibility Hood
    {
        get => hood;
        set => base.PropertyChange(out hood, value);
    }
    private ViewModelBase activeViewModel;
    private readonly IMessenger messenger;

    public ViewModelBase ActiveViewModel
    {
        get { return activeViewModel; }
        set { base.PropertyChange(out this.activeViewModel, value); }
    }
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

    private Command tostore;
    public Command ToStore
    {
        get => new Command(() => ToStoreСommand());
        set => base.PropertyChange(out tostore, value);
    }

    private Command toprofile;
    public Command Toprofile
    {
        get => new Command(() => ToProfile());
        set => base.PropertyChange(out tosettings, value);
    }


    private Command tosettings;
    public Command Tosettings
    {
        get => new Command(() => ToSettingsC());
        set => base.PropertyChange(out tosettings, value);
    }

    private Command tolib;
    public Command Tolib
    {
        get => new Command(() => ToLib());
        set => base.PropertyChange(out tolib, value);
    }

    private Command toWorkShop;
    public Command ToWorkShop
    {
        get => new Command(() => ToWorkShopCommand());
        set => base.PropertyChange(out toWorkShop, value);
    }

    private void ToWorkShopCommand()
    {
        this.messenger.Send(new GetCurrentUser(currentUser));
        this.messenger.Send(new UpdateWorkShop());
        this.messenger.Send(new ViewNavigate(typeof(WorkShopVm)));
    }

    private void ToStoreСommand()
    {
        this.messenger.Send(new GetCurrentUser(currentUser));
        this.messenger.Send(new ViewNavigate(typeof(StoreViewModel)));
    }

    private void ToSettingsC()
    {
        this.messenger.Send(new GetCurrentUser(currentUser));
        this.messenger.Send(new ViewNavigate(typeof(SettingViewVm)));
    }

    private void ToProfile()
    {
        this.messenger.Send(new GetCurrentUser(currentUser));
        this.messenger.Send(new ViewNavigate(typeof(ProfileVm)));
        this.messenger.Send(new UpdateLibary());
    }

    private void ToLib()
    {
        this.messenger.Send(new GetCurrentUser(currentUser));
        this.messenger.Send(new ViewNavigate(typeof(LibaryVm)));
        this.messenger.Send(new UpdateLibary());
    }



    public MainVM(IMessenger messenger)
    {
        this.messenger = messenger;
        this.Hood = Visibility.Hidden;

        this.messenger.Subscribe<ViewNavigate>((message) =>
        {
            if (message is ViewNavigate navigationMessage)
            {
                var viewModelObj = App.ServiceContainer.GetInstance(navigationMessage.DestinationViewModelType);
                if (viewModelObj is ViewModelBase viewModel)
                {
                    this.ActiveViewModel = viewModel;
                }
            }
        });

        messenger.Subscribe<GetCurrentUser>((message) =>
        {
            if (message is GetCurrentUser user)
            {
                currentUser = user.User;
                Avatarurl = currentUser.AvatarUrl;
                Balance = currentUser.Card.Balance;
                Hood = Visibility.Visible;
            }
        });
    }
}

