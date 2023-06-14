using Steam.Messages.Base;
using Steam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Messages;

public class GetCurrentUser : IMessage
{
    public GetCurrentUser(User user)
    {
        User = user;
    }
    public User User { get; set; }  
}
