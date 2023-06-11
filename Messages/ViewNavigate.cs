using Steam.Messages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Messages;

public class ViewNavigate : IMessage
{
    public ViewNavigate(Type destinationViewModelType)
    {
        DestinationViewModelType = destinationViewModelType;
    }

    public Type DestinationViewModelType { get; set; }

}
