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
using System.Windows;

namespace Steam.ViewModel;

public class GameMoreVM : ViewModelBase
{
    public ObservableCollection<Comment> Comments { get; set; } = new ObservableCollection<Comment>();

    private string leavecomment;
    public string Leavecomment
    {
        get => leavecomment;
        set => base.PropertyChange(out leavecomment, value);
    }
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

    private Command buy;
    public Command Buy
    {
        get => new Command(() => BuyCommand());
        set => base.PropertyChange(out buy, value);
    }

    private Command send;
    public Command Send
    {
        get => new Command(() => SendCommand());
        set => base.PropertyChange(out send, value);
    }

    private void SendCommand()
    {
        App.ServiceContainer.GetInstance<EntityFramework>().Comments.Add(new Comment()
        {
            Text = Leavecomment,
            Time = DateTime.Now,
            UserId = currentUser.Id,
            GameId = currentGame.Id,
        });

        App.ServiceContainer.GetInstance<EntityFramework>().SaveChanges();
        Leavecomment = "";
        Update();
    }

    private void BuyCommand()
    {
        var query = App.ServiceContainer.GetInstance<EntityFramework>().Cards.Where(x => x.Id == currentUser.CardId).ToList();
        currentUser.Card = query.First();
        var allusergame = App.ServiceContainer.GetInstance<EntityFramework>().UserGames;
        if (allusergame.Any(x => x.GameId == currentGame.Id && x.UserId == currentUser.Id))
        {
            MessageBox.Show("All ready have!");
            return;
        }
        if (currentUser.Card.Balance < currentGame.Price)
        {
            MessageBox.Show("Balance is too low");
            return;
        }
        currentUser.Card.Balance -= currentGame.Price;
        var usergame = new UserGames()
        {
            GameId = currentGame.Id,
            UserId = currentUser.Id,
        };

        allusergame.Add(usergame);
        App.ServiceContainer.GetInstance<EntityFramework>().SaveChanges();
        MessageBox.Show("All done");
        return;
    }

    private void Update()
    {
        Comments.Clear();
        var comments = App.ServiceContainer.GetInstance<EntityFramework>().Comments.ToList();
        var user = App.ServiceContainer.GetInstance<EntityFramework>().Users.ToList();
        foreach (var item in user)
        {
            foreach(var items in comments)
            {
                if (item.Id == items.UserId && items.GameId == currentGame.Id)
                {
                    Comments.Add(items);
                }
            }
        }
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
                Update();
            }
        });

    }
}
