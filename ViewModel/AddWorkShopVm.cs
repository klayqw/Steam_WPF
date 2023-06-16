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

public class AddWorkShopVm : ViewModelBase
{
    private IMessenger messenger;

    private User currentUser { get; set; }
    private string title;
    public string Title
    {
        get => title;
        set => base.PropertyChange(out title, value);
    }

    private string desc;
    public string Desc
    {
        get => desc;
        set => base.PropertyChange(out desc, value);
    }

    private Command apply;
    public Command Apply
    {
        get => new Command(() => ApplyCommand());
        set => base.PropertyChange(out apply, value);
    }

    private void ApplyCommand()
    {
        if(Title.Length < 0)
        {
            MessageBox.Show("Title cant be zero lenght!");
            return;
        }
       App.ServiceContainer.GetInstance<EntityFramework>().Content.Add(new Content()
       {
           Title = Title,
           Desc = Desc,
           Dislike = 0,
           Like = 0,
       });

        App.ServiceContainer.GetInstance<EntityFramework>().SaveChanges();
        MessageBox.Show("All done");
        this.messenger.Send(new GetCurrentUser(currentUser));
        this.messenger.Send(new ViewNavigate(typeof(WorkShopVm)));
    }

    public AddWorkShopVm(IMessenger messenger)
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
