using Steam.Messages;
using Steam.Models;
using Steam.Service.Base;
using Steam.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.ViewModel;

public class ProfileVm : ViewModelBase
{

    private IMessenger messenger;

    private User currentUser { get; set; }

    private string avatarUrl;
    public string AvatarUrl
    {
        get => avatarUrl;
        set => base.PropertyChange(out  avatarUrl, value);
    }

    private string nickname;
    public string Nickname
    {
        get => nickname;
        set => base.PropertyChange(out nickname, value);
    }


    public ProfileVm(IMessenger messenger)
    {
        this.messenger = messenger;
        messenger.Subscribe<GetCurrentUser>((message) =>
        {
            if (message is GetCurrentUser user)
            {
                currentUser = user.User;
                AvatarUrl = currentUser.AvatarUrl;
                Nickname = currentUser.Nickname;
            }
        });

    }
}
