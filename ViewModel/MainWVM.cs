using Steam.Service.Base;
using Steam.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.ViewModel;

public class MainWVM : ViewModelBase
{
    private readonly IMessenger messenger;

    public MainWVM(IMessenger messenger)
    {
        this.messenger = messenger;
    }
}
