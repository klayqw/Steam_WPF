using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Models;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string AvatarUrl { get; set; }
    public string Nickname { get; set; }
    public string Email { get; set; }
    public int CardId { get; set; }
    public Card Card { get; set; } = new Card();
    public List<Comment> CommentInGame {get;set;} = new List<Comment>();
    public List<UserGames> UserGames {get;set;} = new List<UserGames>();
}
