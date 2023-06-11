using Steam.Messages;
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

public class RegistrationVM : ViewModelBase
{
    private readonly IMessenger messenger;
    private Visibility errorvisibility;
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


    public RegistrationVM(IMessenger mess)
    {
        ErrorVisibility = Visibility.Hidden;
        this.messenger = mess;
    }

}
