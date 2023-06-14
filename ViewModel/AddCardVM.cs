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
        try
        {
            currentUser.Card = new Card()
            {
                NameOnCard = NameOnCard,
                Code = int.Parse(Code),
                CardNumber = NumberOnCard,
                Validity = DateOnCard,
            };
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
