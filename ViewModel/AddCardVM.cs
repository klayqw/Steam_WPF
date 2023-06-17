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

public class AddCardVM : ViewModelBase
{
    private User currentUser { get; set; }
    private string numberoncard;
    public string NumberOnCard
    {
        get => numberoncard;
        set => base.PropertyChange(out numberoncard, value);
    }

    private DateTime dateoncard;
    public DateTime DateOnCard
    {
        get => dateoncard;
        set => base.PropertyChange(out dateoncard, value);
    }
    private string nameoncard;
    public string NameOnCard
    {
        get => nameoncard;
        set => base.PropertyChange(out nameoncard, value);
    }

    private string code;
    public string Code
    {
        get => code;
        set => base.PropertyChange(out code, value);
    }

    private readonly IMessenger messenger;

    private Command apply;
    public Command Apply
    {
        get => new Command(() => ApplyCommand());
        set => base.PropertyChange(out apply, value);
    }

    private void ApplyCommand()
    {
        if(NumberOnCard.Length != 16)
        {
            MessageBox.Show("Error lenght number is not 16!");
            return;
        }
        if(Code.Length != 3)
        {
            MessageBox.Show("Error code is not 3 letter!");
            return;
        }
        if(NameOnCard.Length == 0)
        {
            MessageBox.Show("Error,Name on card cant be 0 lenght!");
            return;
        }
        var query = App.ServiceContainer.GetInstance<EntityFramework>().Cards.Where(x => x.Id == currentUser.CardId).ToList();
        currentUser.Card = query.First();
        try
        {
            currentUser.Card.CardNumber = NumberOnCard;
            currentUser.Card.Validity = DateOnCard;
            currentUser.Card.Code = int.Parse(Code);
            currentUser.Card.NameOnCard = NameOnCard;
            currentUser.Card.Balance = 0;
            App.ServiceContainer.GetInstance<EntityFramework>().SaveChanges();
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
            return;
        }
        MessageBox.Show("All Done");
        return;
    }

    public AddCardVM(IMessenger messenger)
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
