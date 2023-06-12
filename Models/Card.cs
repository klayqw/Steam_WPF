using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Models;

public class Card
{
    public int Id { get; set; }
    public string? CardNumber { get; set; }
    public int Code { get; set; }   
    public DateTime Validity { get; set; }
    public string? NameOnCard { get; set; }
    public double Balance { get; set; }
    
}
