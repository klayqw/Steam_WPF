using Steam.Messages.Base;
using Steam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Messages;

public class GetCurrentGame : IMessage
{
    public Game Game { get; set; }
    public GetCurrentGame(Game game)
    {
        Game = game;
    }
}
