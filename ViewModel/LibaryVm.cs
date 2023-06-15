using Microsoft.EntityFrameworkCore;
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

public class LibaryVm : ViewModelBase
{
    private IMessenger messenger;
    public ObservableCollection<Game> GamesLib { get; set; } = new ObservableCollection<Game>();
    public Game currentGame { get; set; }
  

    private User currentUser { get; set; }

    private Command more;
    public Command More
    {
        get => new Command(() => ShowMore());
        set => base.PropertyChange(out more, value);
    }

    private void ShowMore()
    {
        if(currentGame == null) 
        {
            return;
        }
        Name = currentGame.Name;
        Price = currentGame.Price.ToString();
        Desc = currentGame.Desc;
        ImageUrl = currentGame.ImageUrl;
    }


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
        set => base.PropertyChange(out imageUrl, value);
    }



    private void Update()
    {
        GamesLib.Clear();
        var libary = App.ServiceContainer.GetInstance<EntityFramework>().UserGames.ToList();
        var game = App.ServiceContainer.GetInstance<EntityFramework>().Games.ToList();
        foreach(var item in libary)
        {
           foreach(var items in game)
            {
                if(item.GameId == items.Id && item.UserId == currentUser.Id)
                {
                    GamesLib.Add(items);
                }
            }
        }
    }

    public LibaryVm(IMessenger messenger)
    {
        this.messenger = messenger;
        messenger.Subscribe<GetCurrentUser>((message) =>
        {
            if (message is GetCurrentUser user)
            {
                currentUser = user.User;
            }
        });

        messenger.Subscribe<UpdateLibary>((message) =>
        {
            Update();
        });

    }
}
