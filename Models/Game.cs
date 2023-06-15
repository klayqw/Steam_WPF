using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Models;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
    public List<Comment> GameComments { get; set; } = new List<Comment>();
    public List<UserGames> UserGames { get; set; } = new List<UserGames>();
}
