using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Models;

public class UserComment
{
    public int Id { get; set; }
    public int UserFromId { get; set; }
    public User User { get; set; }
    public int UserToId { get; set; }
    public User UserTo { get; set; }
    public string Text { get; set; }
}
