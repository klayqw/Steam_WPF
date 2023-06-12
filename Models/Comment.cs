using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Models;

public class Comment
{
    public int Id { get; set; } 
    public DateTime Time { get; set; }
    public string Text { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int GameId { get; set; }
    public Game Game { get; set; }
}

