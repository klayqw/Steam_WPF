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

    private void ToStoreСommand()
    {
        this.messenger.Send(new GetCurrentUser(currentUser));
        this.messenger.Send(new ViewNavigate(typeof(StoreViewModel)));
    }

    public GameMoreVM(IMessenger messenger)
    {
        this.messenger = messenger;

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
