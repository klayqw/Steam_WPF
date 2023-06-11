using Steam.Messages;
using Steam.Service;
using Steam.Service.Base;
using Steam.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.ViewModel;

class MainVM : ViewModelBase
{
    private ViewModelBase activeViewModel;
    private readonly IMessenger messenger;

    public ViewModelBase ActiveViewModel
    {
        get { return activeViewModel; }
        set { base.PropertyChange(out this.activeViewModel, value); }
    }

    public MainVM(IMessenger messenger)
    {
        this.messenger = messenger;

        this.messenger.Subscribe<ViewNavigate>((message) =>
        {
            if (message is ViewNavigate navigationMessage)
            {
                var viewModelObj = App.ServiceContainer.GetInstance(navigationMessage.DestinationViewModelType);
                if (viewModelObj is ViewModelBase viewModel)
                {
                    this.ActiveViewModel = viewModel;
                }
            }
        });
    }
}

