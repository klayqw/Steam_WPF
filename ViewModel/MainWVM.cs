using Steam.Messages;
using Steam.Models;
using Steam.Service;
using Steam.Service.Base;
using Steam.View;
using Steam.ViewModel.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.ViewModel;

public class MainWVM : ViewModelBase
{
    private IMessenger messenger;

    private User currentUser { get; set; }

    private Command button;
    public Command Button
    {
        get => new Command(() => buttoncommand());
        set => base.PropertyChange(out button,value);
    }

    private void buttoncommand()
    {
        var uri = "https://open.spotify.com/track/6RqEJvpEzzlwj8g0wKG1ln?si=5fb7c7b1c56946cb";
        var psi = new System.Diagnostics.ProcessStartInfo();
        psi.UseShellExecute = true;
        psi.FileName = uri;
        System.Diagnostics.Process.Start(psi);
    }

    public MainWVM(IMessenger messenger)
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
