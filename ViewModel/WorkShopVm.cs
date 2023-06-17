using Steam.Messages;
using Steam.Models;
using Steam.Service;
using Steam.Service.Base;
using Steam.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.ViewModel;

public class WorkShopVm : ViewModelBase
{
    public ObservableCollection<Content> Contents { get; set; } = new ObservableCollection<Content>();

    private IMessenger messenger;

    private User currentUser { get; set; }

    public Content ContentSelect { get; set; }
    private Command add;
    public Command Add
    {
        get => new Command(() => AddCommand());
        set => base.PropertyChange(out add, value);
    }

    private void AddCommand()
    {
        this.messenger.Send(new GetCurrentUser(currentUser));
        this.messenger.Send(new ViewNavigate(typeof(AddWorkShopVm)));
    }

    private Command more;
    public Command More
    {
        get => new Command(() => MoreCommand());
        set => base.PropertyChange(out add, value);
    }

    private void MoreCommand()
    {
        if (ContentSelect == null)
        {
            return;
        }
        this.messenger.Send(new GetCurrentUser(currentUser));
        this.messenger.Send(new GetCurrentContent(ContentSelect));
        this.messenger.Send(new ViewNavigate(typeof(ContentMoreVm)));
    }



    private void Update()
    {
        Contents.Clear();
        var content = App.ServiceContainer.GetInstance<EntityFramework>().Content.ToList();
        foreach (var items in content)
        {
            Contents.Add(items);
        }
    }   


    public WorkShopVm(IMessenger messenger)
    {
        this.messenger = messenger;
        messenger.Subscribe<GetCurrentUser>((message) =>
        {
            if (message is GetCurrentUser user)
            {
                currentUser = user.User;
            }
        });
        messenger.Subscribe<UpdateWorkShop>((message) =>
        {
           Update();    
        });

    }

}
