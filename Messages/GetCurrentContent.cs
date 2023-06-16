using Steam.Messages.Base;
using Steam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Messages;

public class GetCurrentContent : IMessage
{
    public Content content { get; set; }
    public GetCurrentContent(Content content)
    {
        this.content = content;
    }
}
