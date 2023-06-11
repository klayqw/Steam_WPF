﻿using Steam.Messages;
using Steam.Messages.Base;
using Steam.Service;
using Steam.Service.Base;
using Steam.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Steam.ViewModel;

public class LoginRegistorVM : ViewModelBase
{
    private readonly IMessenger messenger;
    private Visibility errorvisibility;
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

    public LoginRegistorVM(IMessenger messenger)
    {
        ErrorVisibility = Visibility.Hidden;
        this.messenger = messenger;
    }

}
