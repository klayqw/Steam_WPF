using Steam.Messages;
using Steam.Messages.Base;
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
using System.Windows;
using System.Windows.Controls;

namespace Steam.ViewModel;

public class StoreViewModel : ViewModelBase
{
    private User currentUser;
    private IMessenger messenger;
    private Game game;

    public Game Game 
    { 
        get => game;
        set => base.PropertyChange(out game, value);
    }

    private Command more;
    public Command More
    {
        get => new Command(() => ShowMore());
        set => base.PropertyChange(out  more, value);    
    }

    private void ShowMore()
    {
        if (Game == null)
        {
            return;
        }
        messenger.Send(new GetCurrentGame(Game));
        messenger.Send(new ViewNavigate(typeof(GameMoreVM)));
    }
    public ObservableCollection<Game> Games { get; set; } = new ObservableCollection<Game>();


    public StoreViewModel(IMessenger messanger)
    {
        this.messenger = messanger;
        var templist = App.ServiceContainer.GetInstance<EntityFramework>().Games.ToList();
        templist.ForEach(game => Games.Add(game));
        messenger.Subscribe<GetCurrentUser>((message) =>
        {
            if (message is GetCurrentUser user)
            {
                currentUser = user.User;
            }
        });
    }


}
