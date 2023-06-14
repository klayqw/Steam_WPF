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

public class GameMoreVM : ViewModelBase
{
    private Game currentGame { get; set; }
    private User currentUser { get; set; }
    private string name;
    public string Name
    {
        get => name;
        set => base.PropertyChange(out name, value);
    }
    private string desc;
    public string Desc
    {
        get => name;
        set => base.PropertyChange(out desc, value);
    }
    private string price;
    public string Price
    {
        get => price;
        set => base.PropertyChange(out price, value);
    }
    private string imageUrl;
    public string ImageUrl
    {
        get => imageUrl;
        set => base.PropertyChange(out  imageUrl, value); 
    }
   
    private readonly IMessenger messenger;

    public GameMoreVM(IMessenger messenger)
    {
        this.messenger = messenger;

        this.messenger = messenger;
        messenger.Subscribe<GetCurrentUser>((message) =>
        {
            if (message is GetCurrentUser user)
            {
                currentUser = user.User;
            }
        });
        messenger.Subscribe<GetCurrentGame>((message) =>
        {
            if (message is GetCurrentGame game)
            {
                currentGame = game.Game;
                ImageUrl = currentGame.ImageUrl;
                Name = currentGame.Name;
                Desc = currentGame.Desc;
                Price = currentGame.Price.ToString();
            }
        });

    }
}
